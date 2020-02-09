using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Models.Response.Korisnik.Korisnik;

namespace Web.Models.Mapping.Mappers.Korisnik.KorisnikZahtjevKategorijaMap
{
    public static class KorisnikZahtjevKategorijaMapper
    {
        public static IQueryable<KorisnikZahtjevKategorijaModel> ToKorisnikZahtjevKategorijaModel(this IQueryable<Entities.Models.Korisnik.Korisnik> query)
        {
            return query.Select(korisnik => new KorisnikZahtjevKategorijaModel()
            {
                KorisnickoIme = korisnik.KorisnickoIme

            });
        }
    }
}
