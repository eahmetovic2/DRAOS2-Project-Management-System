
using System.Linq;
using Web.Entities.Models.Base;
using Web.Entities.Models.Projekat;
using Web.Models.Response.Base.Projekat;
using Web.Models.Response.Projekat.ZahtjevPrioritet;
using Web.Models.Response.Projekat.ZahtjevTip;
using Web.Models.Response.Zahtjev.ZahtjevStatus;

namespace Web.Models.Mapping.Mappers.Base.ProjekatMap
{
    public static class ZahtjevStatusModelMapper
    {

        public static IQueryable<ZahtjevStatusModel> ToZahtjevStatusModel(this IQueryable<ZahtjevStatus> query)
        {
            return query.Select(zahtjevStatus => new ZahtjevStatusModel()
            {
                Id=zahtjevStatus.Id,
                Naziv = zahtjevStatus.Naziv,
                Default=zahtjevStatus.Default,
                Oznaka=zahtjevStatus.Oznaka
            });
        }

    }
}

