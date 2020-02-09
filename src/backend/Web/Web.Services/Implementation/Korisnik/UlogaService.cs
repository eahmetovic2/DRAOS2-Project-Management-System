using Autofac;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Core.Database;
using Web.Entities;
using Web.Entities.Models.Korisnik;
using Web.Entities.Models.Sifarnik;
using Web.Models.Mapping.Mappers.Korisnik.PravoAkcijaUlogaMap;
using Web.Models.Mapping.Mappers.Korisnik.UlogaMap;
using Web.Models.Request.Korisnik.Uloga;
using Web.Models.Response.Korisnik.PravoAkcijaUloga;
using Web.Models.Response.Korisnik.Uloga;
using Web.Services.Definition.Korisnik;
using Web.Services.Result;

namespace Web.Services.Implementation.Korisnik
{
    public class UlogaService : Service, IUlogaService
    {
        Context context;

        public UlogaService(ILifetimeScope scope, Context context) : base(scope)
        {
            this.context = context;
        }

        public ServiceResult<UlogaListModel> VratiSve()
        {
            var securityLevel = new SecurityLevel { Create = true };

            var query = context.Uloge.Where(a => !a.IsDeleted);
            query = Secure(context.Uloge, securityLevel);

            var items = query.ToUlogaListModelItem().ToList();

            var result = new UlogaListModel
            {
                Items = items
            };

            return Ok(result);
        }

        public ServiceResult<UlogaListModel> VratiSveZaKorisnickoIme(string korisnickoIme)
        {
            var uloge = context.KorisnikUloge.Where(a => a.Korisnik.KorisnickoIme == korisnickoIme)
                                          .Select(a => a.Uloga)
                                          .ToUlogaListModelItem()
                                          .ToList();

            var result = new UlogaListModel
            {
                Items = uloge
            };

            return Ok(result);
        }

        public ServiceResult<PravoAkcijaUlogaListModel> VratiSveDozvoljeneAkcije(int ulogaId)
        {
            var dozvoljeneAkcije = context.PravoAkcijaUloge
                                                  .Where(a => a.UlogaId == ulogaId)
                                                  .ToPravoAkcijaUlogaListModelItem()
                                                  .ToList();

            var result = new PravoAkcijaUlogaListModel
            {
                Items = dozvoljeneAkcije
            };

            return Ok(result);
        }

        public ServiceResult<Nothing> SnimiDozvoljeneAkcije(int ulogaId, SnimiDozvoljeneAkcijeRequestModel model)
        {
            var stareDozvoljeneAkcije = context.PravoAkcijaUloge.Where(a => a.UlogaId == ulogaId)
                                                                .ToList();

            var zaBrisanje = stareDozvoljeneAkcije.Where(st => model.Akcije
                                                                    .All(a => a != st.PravoAkcijaId))
                                                  .ToList();

            var nove = model.Akcije.Where(a => stareDozvoljeneAkcije
                                                    .All(st => st.PravoAkcijaId != a))
                                   .ToList();

            var nevazeciTokeni = context.Tokeni.Where(a => a.UlogaId == ulogaId && a.DatumIsteka > DateTime.Now).ToList();
            foreach (var token in nevazeciTokeni)
            {
                token.DatumIsteka = DateTime.Now.AddMinutes(-1);
            }

            context.PravoAkcijaUloge.RemoveRange(zaBrisanje);

            context.PravoAkcijaUloge.AddRange(
                nove.Select(n => new PravoAkcijaUloga
                {
                    PravoAkcijaId = n,
                    UlogaId = ulogaId
                })
            );

            context.SaveChanges();

            return Ok();
        }

        public ServiceResult<GrupaPravaUlogeListModel> VratiSveGrupePravaProsireno(int ulogaId)
        {
            var grupe = context.PravoGrupe
                        .ToGrupaPravaUlogeListModelItem()
                        .ToList();

            var result = new GrupaPravaUlogeListModel
            {
                Items = grupe
            };

            return Ok(result);
        }

        public ServiceResult<UlogaModel> Kreiraj(KreirajUloguRequestModel model)
        {
            var uloga = new Uloga
            {
                Sifra = model.Sifra,
                Naziv = model.Naziv,
                FrontendModulId = model.FrontendModulId,
                IsDeleted = false
            };
            context.Uloge.Add(uloga);

            context.PravaUpravljanjaKorisnicima.AddRange(model.DozvoljeneUlogeZaUpravljanje.Select(n => new PravoUpravljanjaKorisnikom
            {
                UlogaUpraviteljaId = uloga.Id,
                UlogaUpravljanogId = n
            }));

            SaveChanges(context);

            return VratiPoIdu(uloga.Id);
        }

        public ServiceResult<UlogaModel> Azuriraj(int ulogaId, AzurirajUloguRequestModel model)
        {
            var uloga = context.Uloge.Where(a => a.Id == ulogaId).FirstOrDefault();

            if (uloga == null)
            {
                return NotFound();
            }

            uloga.Sifra = model.Sifra;
            uloga.Naziv = model.Naziv;
            uloga.FrontendModulId = model.FrontendModulId;

            var stareDozvoljeneUloge = context.PravaUpravljanjaKorisnicima
                                              .Where(a => a.UlogaUpraviteljaId == ulogaId)
                                              .ToList();

            var obrisane = stareDozvoljeneUloge.Where(a => !model.DozvoljeneUlogeZaUpravljanje.Contains(a.UlogaUpravljanogId))
                                               .ToList();

            var nove = model.DozvoljeneUlogeZaUpravljanje.Where(a => !stareDozvoljeneUloge.Any(s => s.UlogaUpravljanogId == a))
                                                         .ToList();

            context.PravaUpravljanjaKorisnicima.RemoveRange(obrisane);
            context.PravaUpravljanjaKorisnicima.AddRange(nove.Select(n => new PravoUpravljanjaKorisnikom
            {
                UlogaUpraviteljaId = ulogaId,
                UlogaUpravljanogId = n
            }));

            SaveChanges(context);

            return VratiPoIdu(uloga.Id);
        }

        public ServiceResult<UlogaModel> VratiPoIdu(int ulogaId)
        {
            var uloga = context.Uloge
                                .Include(a => a.PravaUpravljanja)
                                .Where(a => a.Id == ulogaId && !a.IsDeleted)
                                .ToUlogaModel()
                                .FirstOrDefault();

            if (uloga == null)
            {
                return NotFound();
            }

            return Ok(uloga);
        }
    }
}
