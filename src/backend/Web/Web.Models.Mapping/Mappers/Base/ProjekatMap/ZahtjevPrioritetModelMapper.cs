
using System.Linq;
using Web.Entities.Models.Base;
using Web.Entities.Models.Projekat;
using Web.Models.Response.Base.Projekat;
using Web.Models.Response.Projekat.ZahtjevPrioritet;

namespace Web.Models.Mapping.Mappers.Base.ProjekatMap
{
    public static class ZahtjevPrioritetModelMapper
    {

        public static IQueryable<ZahtjevPrioritetModel> ToZahtjevPrioritetModel(this IQueryable<ZahtjevPrioritet> query)
        {
            return query.Select(zahtjevPrioritet => new ZahtjevPrioritetModel()
            {
                Id = zahtjevPrioritet.Id,
                Naziv = zahtjevPrioritet.Naziv,
                Poredak=zahtjevPrioritet.Poredak,
                Default=zahtjevPrioritet.Default
                //ProjekatId = zahtjevPrioritet.ProjekatId
            });
        }

    }
}
