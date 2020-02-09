using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities.Models.Zahtjev;
using Web.Models.Response.Zahtjev.ZahtjevKomentar;

namespace Web.Models.Mapping.Mappers.Base.ZahtjevMap
{
    public static class ZahtjevKomentarModelMapper
    {
        public static IQueryable<ZahtjevKomentarModel> ToZahtjevKomentarModel(this IQueryable<ZahtjevKomentar> query)
        {
            return query.Select(zahtjevKomentar => new ZahtjevKomentarModel()
            {
                Id = zahtjevKomentar.Id,
                Komentar = zahtjevKomentar.Komentar,
                CreatedBy = zahtjevKomentar.CreatedBy,
                DatumKreiranja = zahtjevKomentar.DatumKreiranja

            });
        }

        public static IQueryable<ZahtjevKomentarListModelItem> ToZahtjevKomentarListModelItem(this IQueryable<ZahtjevKomentar> query)
        {
            return query.Select(zahtjevKomentar => new ZahtjevKomentarListModelItem()
            {
                Id = zahtjevKomentar.Id,
                Komentar = zahtjevKomentar.Komentar,
                CreatedBy = zahtjevKomentar.CreatedBy,
                DatumKreiranja = zahtjevKomentar.DatumKreiranja

            });
        }
    }
}
