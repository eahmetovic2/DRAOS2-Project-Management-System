using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities.Models.Korisnik;
using Web.Models.Response.Korisnik.PravoAkcija;
using Web.Models.Response.Korisnik.PravoObjekt;
using Web.Models.Response.Korisnik.Uloga;

namespace Web.Models.Mapping.Mappers.Korisnik.UlogaMap
{
    public static class UlogaMapper
    {
        public static IQueryable<UlogaModel> ToUlogaModel(this IQueryable<Entities.Models.Sifarnik.Uloga> query)
        {
            return query.Select(value => new UlogaModel
            {
                Sifra = value.Sifra,
                Naziv = value.Naziv,
                FrontendModulId = value.FrontendModulId,
                DozvoljeneUlogeZaUpravljanje = value.PravaUpravljanja.Select(a => a.UlogaUpravljanogId).ToList(),
                Id = value.Id
            });
        }

        public static IQueryable<UlogaListModelItem> ToUlogaListModelItem(this IQueryable<Entities.Models.Sifarnik.Uloga> query)
        {
            return query.Select(value => new UlogaListModelItem
            {
                Id = value.Id,
                Naziv = value.Naziv,
                VrijednostUAplikaciji = value.VrijednostUAplikaciji
            });
        }

        public static IQueryable<GrupaPravaUlogeListModelItem> ToGrupaPravaUlogeListModelItem(this IQueryable<PravoGrupa> query)
        {
            return query.Select(value => new GrupaPravaUlogeListModelItem
            {
                Id = value.Id,
                Naziv = value.Naziv,
                PravoObjekti = new PravoObjektListModel
                {
                    Items = value.PravoObjekti.Select(objekat => new PravoObjektListModelItem
                    {
                        Id = objekat.Id,
                        Naziv = objekat.Naziv,
                        PravoAkcije = new PravoAkcijaListModel
                        {
                            Items = objekat.PravoAkcije.Select(akcija => new PravoAkcijaListModelItem
                            {
                                Id = akcija.Id,
                                Naziv = akcija.Naziv,
                                Opis = akcija.Opis,
                                Sifra = akcija.Sifra
                            }).ToList()
                        }
                    }).ToList()
                }
            });
        }
    }
}
