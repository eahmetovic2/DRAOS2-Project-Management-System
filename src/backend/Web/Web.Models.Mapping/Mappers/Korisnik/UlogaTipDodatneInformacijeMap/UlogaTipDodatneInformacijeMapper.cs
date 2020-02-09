using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities.Models.Korisnik;
using Web.Models.Response.Korisnik.UlogaTipDodatneInformacije;

namespace Web.Models.Mapping.Mappers.Korisnik.UlogaTipDodatneInformacijeMap
{
    public static class UlogaTipDodatneInformacijeMapper
    {
        public static IQueryable<UlogaTipDodatneInformacijeListModelItem> ToUlogaTipDodatneInformacijeListModelItem(this IQueryable<UlogaTipDodatneInformacije> query)
        {
            return query.Select(value => new UlogaTipDodatneInformacijeListModelItem
            {
                KorisnikTipDodatneInformacijeId = value.KorisnikTipDodatneInformacijeId,
                UlogaId = value.UlogaId,
                Sifra = value.KorisnikTipDodatneInformacije.Sifra
            });
        }
    }
}
