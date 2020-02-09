using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities.Models.Projekat;
using Web.Models.Response.Zahtjev;

namespace Web.Models.Mapping.Mappers.Base.ZahtjevMap
{
    public static class ZahtjevStatusModelMapper
    {
        public static IQueryable<ZahtjevStatusModel> ToZahtjevStatusModel(this IQueryable<ZahtjevStatus> query)
        {
            return query.Select(zahtjevStatus => new ZahtjevStatusModel()
            {
                Id = zahtjevStatus.Id,
                Naziv = zahtjevStatus.Naziv,
                Oznaka= zahtjevStatus.Oznaka

            });
        }
    }
}
