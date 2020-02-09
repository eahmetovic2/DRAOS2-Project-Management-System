using Autofac;
using Web.Core.Constants;
using Web.Entities;
using Web.Entities.Models.Korisnik;
using Web.Models.Request.Korisnik;
using Web.Models.Response.Token;
using Web.Services.Result;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Web.Models.Mapping.Mappers.TokenMap;
using Web.Services.Definition.Korisnik;

namespace Web.Services.Implementation
{
    /// <summary>
    /// Implementacija servisa koji radi sa osobama
    /// </summary>
    public class TokenService : Service, ITokenService
    {
        /// <summary>
        /// Entity framework db kontekst 
        /// </summary>
        private Context context;

        /// <summary>
        /// Servis za rad sa postavkama
        /// </summary>
        private IPostavkeService postavkeService;

   

        IKorisnikService korisnikService;

        IUlogaService ulogaService;

        /// <summary>
        /// Konstruktor servisa
        /// </summary>
        public TokenService(ILifetimeScope scope, Context context, IPostavkeService postavkeService, IKorisnikService korisnikService, IUlogaService ulogaService)
            : base(scope)
        {
            this.context = context;
            this.postavkeService = postavkeService;         
            this.korisnikService = korisnikService;
            this.ulogaService = ulogaService;
        }

        public ServiceResult<TokenListModel> VratiTokenePoKorisnickomImenu(String korisnickoIme, ListaTokenaRequestModel model)
        {
            // vrati tokene za datog korisnika koji nisu istekli
            var datumIsteka = DateTime.Now;
            var query = context.Tokeni
                .Where(t =>
                    t.VlasnikKorisnickoIme == korisnickoIme &&
                    t.DatumIsteka > datumIsteka);

            // uradi stranicenje
            var total = query.Count();
            var tokeni = query
                .OrderByDescending(t => t.DatumPosljednjeAkcije)
                .Skip(model.Page * model.Count - model.Count)
                .Take(model.Count)
                .ToTokenListModelItem()
                .ToList();

            var result = new TokenListModel
            {
                Items = tokeni,
                Page = model.Page,
                Total = total
            };

            return Ok(result);
        }

        public ServiceResult<TokenModel> VratiTokenPoIdu(String korisnickoIme, Guid tokenId)
        {
            // dobavi token za korisnika ako postoji
            var token = context.Tokeni
                .Include(t => t.Vlasnik)
                    .ThenInclude(a => a.KorisnikUloge)
                        .ThenInclude(t => t.KorisnikUlogaDodatnaInformacija)
                            .ThenInclude(t => t.KorisnikTipDodatneInformacije)
                .SingleOrDefault(t =>
                    t.VlasnikKorisnickoIme == korisnickoIme &&
                    t.Id == tokenId);

            // uradi validaciu tokena
            return ValidirajToken(token, null, null);
        }

        public int BrojOnlineKorisnika()
        {
            var tokeni = context.Tokeni.Where(a => a.DatumIsteka > DateTime.Now && a.Tip == TokenTip.Sesija && !a.Vlasnik.Onemogucen).GroupBy(v => v.VlasnikKorisnickoIme).ToList();
            return tokeni.Count();
        }

        public ServiceResult<TokenModel> ValidirajToken(Guid tokenId, String ip, String klijent)
        {
            // dobavi token po id-u ako postoji
            var token = context.Tokeni
                .Include(t => t.Vlasnik)
                    .ThenInclude(a => a.KorisnikUloge)
                        .ThenInclude(a => a.KorisnikUlogaDodatnaInformacija)
                            .ThenInclude(a => a.KorisnikTipDodatneInformacije)
                .SingleOrDefault(t => t.Id == tokenId);

            // uradi pravu validaciju tokena
            return ValidirajToken(token, ip, klijent);
        }

        private ServiceResult<TokenModel> ValidirajToken(Token token, String ip, String klijent)
        {
            // provjeri token
            if (token == null)
                return NotFound();

            var trenutnaUloga = context.Uloge.Include(a => a.PravoAkcijaUloge)
                                                .ThenInclude(a => a.PravoAkcija)
                                             .Where(a => a.Id == token.UlogaId)
                                             .FirstOrDefault();

            // ucitaj samo one korisnik uloge koje trebaju
            context.Entry(trenutnaUloga).Collection(a => a.KorisnikUloge)
                                                 .Query()
                                                 .Where(a => a.KorisnickoIme == token.VlasnikKorisnickoIme)
                                                 .Include(a => a.KorisnikUlogaDodatnaInformacija)
                                                 .ThenInclude(a => a.KorisnikTipDodatneInformacije)
                                                 .Load();

            token.Vlasnik.TrenutnaUloga = trenutnaUloga;

            if (token.DatumIsteka < DateTime.Now)
                return ValidationError("Token je istekao.");

            if (token.Vlasnik.Onemogucen)
                return ValidationError("Vlasnik tokena je onemogucen.");

            // ako su postavljeni ip ili adresa, uradi azuriranje tokena
            if (!String.IsNullOrWhiteSpace(ip) ||
                !String.IsNullOrWhiteSpace(klijent))
            {
                // koristimo sistemske postavke za trajanje sesije
                var postavke = postavkeService.VratiPostavke();
                if (postavke.IsOk)
                {
                    token.DatumIsteka = DateTime.Now.AddDays(
                        postavke.Value.TrajanjeSesije);
                    token.DatumPosljednjeAkcije = DateTime.Now;

                    token.PosljednjiIp = ip;
                    token.PosljednjiKlijent = klijent;

                    SaveChanges(context);
                }
            }

            var frontendModul = context.Uloge.Where(a => a.Id == token.UlogaId)
                                             .Select(a => a.FrontendModul)
                                             .First();

            token.FrontendModul = frontendModul.Sifra;
            token.FrontendModulNaslov = frontendModul.Naziv;

            // uradi mapiranje
            var result = token.ToTokenModel();

            return Ok(result);
        }

		public ServiceResult<TokenModel> KreirajToken(String korisnickoIme, int ulogaId, String ip, String klijent, Core.Constants.TokenTip Tip = Core.Constants.TokenTip.Sesija)        
        {
            var vlasnik = korisnikService.VratiKorisnikaPoKorisnickomImenu(korisnickoIme);
            if (!vlasnik.IsOk)
                return MissingEntity("Vlasnik");

            if (vlasnik.Value.Onemogucen)
                return ValidationError("Vlasnik tokena je onemogucen.");

            // koristimo sistemske postavke za trajanje sesije
            var postavke = postavkeService.VratiPostavke();
            if (!postavke.IsOk)
                return MissingEntity("Postavke");

            var uloge = ulogaService.VratiSveZaKorisnickoIme(korisnickoIme);
            if (!uloge.IsOk)
            {
                return Error("Nije moguće dobaviti uloge korisnika");
            }

            //todo ovo provjeriti da li je ok
            if (Tip == TokenTip.Temp)
            {
                ulogaId = uloge.Value.Items.First().Id;
            }

            if (uloge.Value.Items.All(a => a.Id != ulogaId))
            {
                return Error("Korisnik nije u datoj ulozi.");
            }

            var datumIsteka = DateTime.Now.AddDays(postavke.Value.TrajanjeSesije);
           
            // kreiraj entitet
            var token = new Token()
            {
                Id = Guid.NewGuid(),
                VlasnikKorisnickoIme = korisnickoIme,
                DatumKreiranja = DateTime.Now,
                DatumIsteka = datumIsteka,
                DatumPosljednjeAkcije = DateTime.Now,
                UlogaId = ulogaId,
                Tip = Tip
            };

            // spasi token
            context.Tokeni.Add(token);
            SaveChanges(context);

            // uradi validaciju i vrati rezultat
            var result = ValidirajToken(token.Id, ip, klijent);

            if (!result.IsOk)
            {
                return result;
            }

            return result;
        }

        public ServiceResult<Nothing> ObrisiToken(String korisnickoIme, Guid tokenId)
        {
            // dobavi token ako postoji
            var token = context.Tokeni
                .SingleOrDefault(t =>
                    t.VlasnikKorisnickoIme == korisnickoIme &&
                    t.Id == tokenId);
            if (token == null)
                return NotFound();

            context.Tokeni.Remove(token);
            SaveChanges(context);

            // sve je ok
            return Ok();
        }
    }
}
