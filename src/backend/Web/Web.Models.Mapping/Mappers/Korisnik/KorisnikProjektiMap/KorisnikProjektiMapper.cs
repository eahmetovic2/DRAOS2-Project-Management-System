using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities.Models.Korisnik;
using Web.Entities.Models.Projekat;
using Web.Models.Response.Korisnik.KorisnikProjekti;

namespace Web.Models.Mapping.Mappers.Korisnik.KorisnikProjektiMap
{
    public static class KorisnikProjektiMapper
    {
        public static IQueryable<KorisnikProjektiModel> ToKorisnikProjektiModel(this IQueryable<Projekat> query)
        {
            return query.Select(value => new KorisnikProjektiModel
            {
                Id=value.Id,
                Naziv=value.Naziv
            });
        }
    }
}
