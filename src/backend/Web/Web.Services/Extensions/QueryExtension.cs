using Web.Models.Request.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Models.Response.Sifarnik;
using Web.Entities.Models;
using Web.Entities.Models.Base;

namespace Web.Services.Extensions
{
    public static class QueryExtension
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, PagedRequestModel pagedModel)
        {
            return pagedModel.Sve ? source
                                  : source.Skip(pagedModel.Page * pagedModel.Count - pagedModel.Count)
                                          .Take(pagedModel.Count);
        }

        public static SifarnikList ToSifarnikList(this IEnumerable<SifarnikModel> source)
        {
            var lista = new SifarnikList();

            foreach (SifarnikModel item in source.ToList())
            {
                lista.Add(item);
            }

            var najnoviji = source.Max(a => a.DatumIzmjene);

            lista.DatumResponsa = najnoviji.HasValue ? najnoviji.Value : new DateTime();

            return lista;
        }
        
    }
}
