using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities.Models.Projekat;
using Web.Models.Response.Projekat.ProjekatKonfiguracija;

namespace Web.Models.Mapping.Mappers.Base.ProjekatMap
{
    public static class ProjekatKonfiguracijaModelMapper
    {
        public static IQueryable<ProjekatKonfiguracijaModel> ToProjekatKonfiguracijaModel(this IQueryable<ProjekatKonfiguracija> query)
        {
            return query.Select(projekatKonfiguracija => new ProjekatKonfiguracijaModel()
            {
                Id = projekatKonfiguracija.Id,
                RadnoVrijemeOd= projekatKonfiguracija.RadnoVrijemeOd,
                RadnoVrijemeDo = projekatKonfiguracija.RadnoVrijemeDo,
                RadniDani= projekatKonfiguracija.RadniDani
            });
        }

        public static ProjekatKonfiguracijaModel ToProjekatKonfiguracijaModel(this ProjekatKonfiguracija query)
        {
            return new ProjekatKonfiguracijaModel()
            {
                Id = query.Id,
                RadnoVrijemeOd = query.RadnoVrijemeOd,
                RadnoVrijemeDo = query.RadnoVrijemeDo,
                RadniDani = query.RadniDani
            };
        }
    }
}
