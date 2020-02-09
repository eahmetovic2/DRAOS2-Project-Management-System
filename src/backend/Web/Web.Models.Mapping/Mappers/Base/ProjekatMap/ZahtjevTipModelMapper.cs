
using System.Linq;
using Web.Entities.Models.Base;
using Web.Entities.Models.Projekat;
using Web.Models.Response.Base.Projekat;
using Web.Models.Response.Projekat.ZahtjevPrioritet;
using Web.Models.Response.Projekat.ZahtjevTip;

namespace Web.Models.Mapping.Mappers.Base.ProjekatMap
{
    public static class ZahtjevTipModelMapper
    {

        public static IQueryable<ZahtjevTipModel> ToZahtjevTipModel(this IQueryable<ZahtjevTip> query)
        {
            return query.Select(zahtjevTip => new ZahtjevTipModel()
            {
                Id=zahtjevTip.Id,
                Naziv = zahtjevTip.Naziv,
                Default=zahtjevTip.Default
                
            });
        }

    }
}

