using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities.Models.Korisnik;
using Web.Models.Response.Korisnik.KorisnikDodatneInformacije;
using Web.Models.Response.Korisnik.KorisnikTipDodatneInformacije;

namespace Web.Models.Mapping.Mappers.Korisnik.KorisnikDodatneInformacijeMap
{
    public static class KorisnikDodatneInformacijeMapper
    {
        public static IQueryable<KorisnikDodatneInformacijeListModelItem> ToKorisnikDodatneInformacijeListModelItem(this IQueryable<KorisnikUlogaDodatnaInformacija> query)
        {
            return query.Select(value => new KorisnikDodatneInformacijeListModelItem
            {
                KorisnickoIme = value.KorisnikUloga.KorisnickoIme,
                TipDodatneInformacije = new KorisnikTipDodatneInformacijeModel
                {
                    Id = value.KorisnikTipDodatneInformacije.Id,
                    Naziv = value.KorisnikTipDodatneInformacije.Naziv,
                    Onemogucen = value.KorisnikTipDodatneInformacije.Onemogucen,
                    Opis = value.KorisnikTipDodatneInformacije.Opis,
                    Poredak = value.KorisnikTipDodatneInformacije.Poredak,
                    Sifra = value.KorisnikTipDodatneInformacije.Sifra
                },
                Vrijednost = value.Vrijednost
            });
        }
    }
}
