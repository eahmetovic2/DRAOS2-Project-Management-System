using Autofac;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Core.Constants;
using Web.Core.Database;
using Web.Entities;
using Web.Entities.Models.Base;
using Web.Entities.Models.Projekat;
using Web.Models.Mapping.Mappers.Base.ProjekatMap;
using Web.Models.Mapping.Mappers.Korisnik.KorisnikProjektiMap;
using Web.Models.Mapping.Mappers.KorisnikMap;
using Web.Models.Request.Base.Projekat;
using Web.Models.Request.Korisnik;
using Web.Models.Request.Projekat.Projekat;
using Web.Models.Response.Base.Projekat;
using Web.Models.Response.Korisnik.Korisnik;
using Web.Models.Response.Korisnik.KorisnikProjekti;
using Web.Models.Response.Projekat.Projekat;
using Web.Services.Definition.Base;
using Web.Services.Result;

namespace Web.Services.Implementation.Base
{
    public class ProjekatService : Service, IProjekatService
    {

        /// <summary>
        /// Entity framework db kontekst 
        /// </summary>
        private Context context;


        private IAuthService authService;

        /// <summary>
        /// 
        /// </summary>


        private IApplicationConfigurationService applicationConfigurationService;


        /// <summary>
        /// Konstruktor servisa
        /// </summary>
        public ProjekatService(ILifetimeScope scope, Context context, IAuthService authService, IApplicationConfigurationService applicationConfigurationService)
            : base(scope)
        {
            this.context = context;
            this.authService = authService;
            this.applicationConfigurationService = applicationConfigurationService;

        }

        public ServiceResult<ProjekatListModel> VratiSveProjekte(ListaProjekataRequestModel model)
        {
            var projekti = new List<ProjekatListModelItem>();

            var total = 0;

            var trenutni = authService.TrenutniKorisnik();

            if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Administrator)
            {
                projekti = context.Projekti.ToProjekatListModelItem().ToList();
                total = projekti.Count();

                if (!model.Sve)
                {
                    projekti = context.Projekti
                        .Skip(model.Page * model.Count - model.Count)
                        .Take(model.Count).ToProjekatListModelItem().ToList();
                }

            }

            else
            {
                var ulogaId = context.Uloge.Where(a => a.VrijednostUAplikaciji == trenutni.TrenutnaUlogaId).Select(a => a.Id).FirstOrDefault();

                var korisnikUlogaId = context.KorisnikUloge.Where(a => a.KorisnickoIme == trenutni.KorisnickoIme && a.UlogaId == trenutni.TrenutnaUlogaId).Select(a => a.KorisnikUlogaId).FirstOrDefault();

                projekti = context.KorisnikProjekti.Where(a => a.KorisnikUlogaId == korisnikUlogaId).Select(a => a.Projekat).ToProjekatListModelItem().ToList();
                total = projekti.Count();

                if (!model.Sve)
                {
                    projekti = context.KorisnikProjekti.Where(a => a.KorisnikUlogaId == korisnikUlogaId).Select(a => a.Projekat).Skip(model.Page * model.Count - model.Count)
                        .Take(model.Count).ToProjekatListModelItem().ToList();
                }
            }

            if (projekti == null)
                return NotFound();

            var result = new ProjekatListModel
            {
                Items = projekti,
                Page = model.Page,
                Total = total
            };
            return Ok(result);
        }

        public ServiceResult<ProjekatModel> Kreiraj(KreirajProjekatRequestModel model)
        {

            //model.KorisnickoIme = model.KorisnickoIme.Trim().ToLower();
            var trenutni = authService.TrenutniKorisnik();

            if (trenutni.TrenutnaUloga.VrijednostUAplikaciji != (int)Uloga.Administrator)
                return Error("Nemate pravo kreiranja projekta");

            if (context.Projekti.FirstOrDefault(x => x.Naziv == model.Naziv) != null)
                return Error("Projekat pod tim imenom već postoji.");

            if (string.IsNullOrEmpty(model.Naziv))
                return Error("Polje naziv projekta ne može biti prazno");

            /*var trenutni = authService.TrenutniKorisnik();


            var hashProvider = Scope.Resolve<IHashProvider>();
            var tajna = hashProvider.HashPassword(model.Lozinka);*/

            Projekat projekat = new Projekat();

            projekat.Naziv = model.Naziv;
            projekat.Opis = model.Opis;
            projekat.DatumKreiranja = DateTime.Now;

            #region ProjekatKonfiguracija
            ProjekatKonfiguracija projekatKonfiguracija = new ProjekatKonfiguracija();
            projekatKonfiguracija.RadnoVrijemeOd = new TimeSpan(8, 0, 0);
            projekatKonfiguracija.RadnoVrijemeDo = new TimeSpan(16, 0, 0);
            projekatKonfiguracija.RadniDani = "1111100";

            projekat.ProjekatKonfiguracija = projekatKonfiguracija;
            #endregion

            #region Prioriteti zahtjeva
            ZahtjevPrioritet zahtjevPrioritetNizak = new ZahtjevPrioritet();
            zahtjevPrioritetNizak.Default = false;
            zahtjevPrioritetNizak.Naziv = "Nizak";
            zahtjevPrioritetNizak.Poredak = 2;


            ZahtjevPrioritet zahtjevPrioritetSrednji = new ZahtjevPrioritet();
            zahtjevPrioritetSrednji.Default = true;
            zahtjevPrioritetSrednji.Naziv = "Srednji";
            zahtjevPrioritetSrednji.Poredak = 1;

            ZahtjevPrioritet zahtjevPrioritetVisok = new ZahtjevPrioritet();
            zahtjevPrioritetVisok.Default = false;
            zahtjevPrioritetVisok.Naziv = "Visok";
            zahtjevPrioritetVisok.Poredak = 0;

            projekat.PrioritetiZahtjeva = new List<ZahtjevPrioritet>();
            projekat.PrioritetiZahtjeva.Add(zahtjevPrioritetNizak);
            projekat.PrioritetiZahtjeva.Add(zahtjevPrioritetSrednji);
            projekat.PrioritetiZahtjeva.Add(zahtjevPrioritetVisok);
            #endregion
            #region Tipovi zahtjeva

            ZahtjevTip zahtjevTipBug = new ZahtjevTip();
            zahtjevTipBug.Default = false;
            zahtjevTipBug.Naziv = "Bug";


            ZahtjevTip zahtjevTipZahtjev = new ZahtjevTip();
            zahtjevTipZahtjev.Default = true;
            zahtjevTipZahtjev.Naziv = "Zahtjev";

            ZahtjevTip zahtjevTipZadatak = new ZahtjevTip();
            zahtjevTipZadatak.Default = false;
            zahtjevTipZadatak.Naziv = "Zadatak";

            ZahtjevTip zahtjevTipPrica = new ZahtjevTip();
            zahtjevTipPrica.Default = false;
            zahtjevTipPrica.Naziv = "Prica";

            projekat.TipoviZahtjeva = new List<ZahtjevTip>();
            projekat.TipoviZahtjeva.Add(zahtjevTipBug);
            projekat.TipoviZahtjeva.Add(zahtjevTipZahtjev);
            projekat.TipoviZahtjeva.Add(zahtjevTipZadatak);
            projekat.TipoviZahtjeva.Add(zahtjevTipPrica);

            #endregion
            #region StatusiZahtjeva

            ZahtjevStatus zahtjevStatusZavrsen = new ZahtjevStatus();
            zahtjevStatusZavrsen.Default = false;
            zahtjevStatusZavrsen.Naziv = "Završen";
            zahtjevStatusZavrsen.Oznaka = (int)OznakeStatusa.Done;
            zahtjevStatusZavrsen.Poredak = 2;


            ZahtjevStatus zahtjevStatusRadiSe = new ZahtjevStatus();
            zahtjevStatusRadiSe.Default = false;
            zahtjevStatusRadiSe.Naziv = "Radi se";
            zahtjevStatusRadiSe.Oznaka = (int)OznakeStatusa.InProgress;
            zahtjevStatusRadiSe.Poredak = 1;



            ZahtjevStatus zahtjevStatusPotrebnoUraditi = new ZahtjevStatus();
            zahtjevStatusPotrebnoUraditi.Default = true;
            zahtjevStatusPotrebnoUraditi.Naziv = "Potrebno uraditi";
            zahtjevStatusPotrebnoUraditi.Oznaka = (int)OznakeStatusa.ToDo;
            zahtjevStatusPotrebnoUraditi.Poredak = 0;



            projekat.StatusiZahtjeva = new List<ZahtjevStatus>();
            projekat.StatusiZahtjeva.Add(zahtjevStatusZavrsen);
            projekat.StatusiZahtjeva.Add(zahtjevStatusRadiSe);
            projekat.StatusiZahtjeva.Add(zahtjevStatusPotrebnoUraditi);
            #endregion

            DioProjekta dioProjekta = new DioProjekta();
            dioProjekta.Naziv = "Osnovni";

            #region KategorijeZahtjeva
            ZahtjevKategorija zahtjevKategorija = new ZahtjevKategorija();
            zahtjevKategorija.Naziv = "Osnovna";

            /*ZahtjevKategorija zahtjevKategorijaNeodredjena = new ZahtjevKategorija();
            zahtjevKategorijaNeodredjena.Naziv = "Neodređena";


            ZahtjevKategorija zahtjevKategorijaSporedna = new ZahtjevKategorija();
            zahtjevKategorijaSporedna.Naziv = "Sporedna";*/

            dioProjekta.KategorijeZahtjeva = new List<ZahtjevKategorija>();

            dioProjekta.KategorijeZahtjeva.Add(zahtjevKategorija);
            /*dioProjekta.KategorijeZahtjeva.Add(zahtjevKategorijaNeodredjena);
            dioProjekta.KategorijeZahtjeva.Add(zahtjevKategorijaSporedna);*/

            #endregion

            projekat.DijeloviProjekta = new List<DioProjekta>();
            projekat.DijeloviProjekta.Add(dioProjekta);

            context.Add(dioProjekta);

            context.Add(projekat);

            SaveChanges(context);

            return VratiProjekatPoNazivu(projekat.Naziv);

        }

        public ServiceResult<ProjekatModel> VratiProjekatPoNazivu(string naziv)
        {

            var projekat = context.Projekti
                            .ToProjekatModel()
                            .SingleOrDefault(p => p.Naziv == naziv);

            if (projekat == null)
                return NotFound();

            return Ok(projekat);
        }

        public ServiceResult<ProjekatModel> AzurirajNazivProjekta(string stariNaziv, AzurirajNazivProjektaRequestModel model)
        {

            if (string.IsNullOrEmpty(model.Naziv))
                return Error("Polje naziv projekta ne može biti prazno");

            var projekat = context.Projekti
                            .ToProjekatModel()
                            .SingleOrDefault(p => p.Naziv == stariNaziv);

            if (projekat == null)
                return NotFound();
            projekat.Naziv = model.Naziv;
            SaveChanges(context);

            // uradi mapiranje
            return Ok(projekat);
        }

        public ServiceResult<ProjekatModel> AzurirajProjekat(int projekatId, AzurirajProjekatRequestModel model)
        {

            var projekat = context.Projekti
                            .SingleOrDefault(p => p.Id == projekatId);

            if (projekat == null)
                return NotFound();
            projekat.Naziv = model.Naziv;
            projekat.Opis = model.Opis;
            projekat.DatumIzmjene = DateTime.Now;

            SaveChanges(context);

            var obj = context.Projekti
                .ToProjekatModel()
                .SingleOrDefault(p => p.Id == projekatId);

            return Ok(obj);
        }



        public ServiceResult<Nothing> ObrisiProjekat(int id)
        {
            var projekat = context.Projekti.AsQueryable().
                SingleOrDefault(p => p.Id == id);
            if (projekat == null)
                return NotFound();
            context.Remove(projekat);
            SaveChanges(context);
            return Ok();
        }

        public ServiceResult<ProjekatModel> VratiProjekat(int projekatId)
        {
            var korisnik = authService.TrenutniKorisnik();

            if (korisnik.TrenutnaUloga.VrijednostUAplikaciji != (int)Uloga.Administrator)
            {
                var ulogaId = korisnik.TrenutnaUloga.Id;

                var korisnikUlogaId = context.KorisnikUloge.Where(a => a.KorisnickoIme == korisnik.KorisnickoIme && a.UlogaId == ulogaId).Select(a => a.KorisnikUlogaId).FirstOrDefault();
                var korisnikProjekat = context.KorisnikProjekti.Where(p => p.ProjekatId == projekatId && p.KorisnikUlogaId == korisnikUlogaId);
                if (!korisnikProjekat.Any())
                    return Error("Nemate pravo pregleda ovog projekta.");
            }
            var projekat = context.Projekti.ToProjekatModel().
                    SingleOrDefault(p => p.Id == projekatId);

            if (projekat == null)
                return NotFound();

            /*var securityLevel = new SecurityLevel();
            //var query = context.Projekti.Where(p => p.Id == projekatId).AsQueryable();
            var projekat = Secure1(projekatId, securityLevel);
            if(projekat.Count()==0)
            {
                return Error("Nemate pravo pristupa.");
            }*/
            return Ok(projekat);
        }

        public ServiceResult<KorisnikListModel> VratiSveKorisnikeProjekta(int projekatId, ListaKorisnikaRequestModel model)
        {


            var korisnici = context.KorisnikProjekti.Where(k => k.ProjekatId == projekatId).ToProjekatKorisniciModelItem().ToList();

            var total = korisnici.Count();

            korisnici = korisnici.Skip(model.Page * model.Count - model.Count)
                    .Take(model.Count).ToList();


            var result = new KorisnikListModel
            {
                Items = korisnici,
                Page = model.Page,
                Total = total
            };
            return Ok(result);
        }

        public ServiceResult<List<KorisnikProjektiModel>> VratiSveProjekteZaKorisnikUlogu(String korisnickoIme, int ulogaId)
        {

            var korisnikUlogaId = context.KorisnikUloge.Where(a => a.KorisnickoIme == korisnickoIme && a.UlogaId == ulogaId).Select(a => a.KorisnikUlogaId).FirstOrDefault();
            var projekti = context.KorisnikProjekti.Where(k => k.KorisnikUlogaId == korisnikUlogaId).Select(p => p.Projekat).ToKorisnikProjektiModel().ToList();

            /*projekti = projekti.Skip(model.Page * model.Count - model.Count)
                    .Take(model.Count).ToList();*/


            /*var result = new KorisnikListModel
            {
                Items = projekti,
                Page = model.Page,
                Total = total
            };*/
            return Ok(projekti);
        }
    }
}
