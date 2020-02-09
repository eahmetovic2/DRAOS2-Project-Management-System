using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities.Models.Projekat;
using Web.Models.Response.Projekat.DioProjekta;

namespace Web.Models.Mapping.Mappers.Base.ProjekatMap
{
    public static class DioProjektaModelMapper
    {
        public static IQueryable<DioProjektaModel> ToDioProjektaModel(this IQueryable<DioProjekta> query)
        {
            return query.Select(dioProjekta => new DioProjektaModel()
            {
                Id = dioProjekta.Id,
                Naziv = dioProjekta.Naziv

            });
        }
    }
}
