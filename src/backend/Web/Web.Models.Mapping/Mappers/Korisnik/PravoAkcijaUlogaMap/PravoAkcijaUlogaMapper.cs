using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities.Models.Korisnik;
using Web.Models.Response.Korisnik.PravoAkcijaUloga;

namespace Web.Models.Mapping.Mappers.Korisnik.PravoAkcijaUlogaMap
{
    public static class PravoAkcijaUlogaMapper
    {
        public static IQueryable<PravoAkcijaUlogaListModelItem> ToPravoAkcijaUlogaListModelItem(this IQueryable<PravoAkcijaUloga> query)
        {
            return query.Select(value => new PravoAkcijaUlogaListModelItem
            {
                PravoAkcijaId = value.PravoAkcijaId,
                UlogaId = value.UlogaId
            });
        }
    }
}
