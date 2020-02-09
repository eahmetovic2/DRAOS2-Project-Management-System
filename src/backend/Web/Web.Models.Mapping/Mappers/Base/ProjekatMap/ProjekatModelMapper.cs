
using System.Linq;
using Web.Entities.Models.Base;
using Web.Entities.Models.Projekat;
using Web.Models.Response.Base.Projekat;
using Web.Models.Response.Korisnik.Korisnik;

namespace Web.Models.Mapping.Mappers.Base.ProjekatMap
{
    public static class ProjekatModelMapper
    {

        public static IQueryable<ProjekatModel> ToProjekatModel(this IQueryable<Projekat> query)
        {
            return query.Select(projekat => new ProjekatModel()
            {
                Id=projekat.Id,
                Naziv = projekat.Naziv,
                Opis = projekat.Opis
            });
        }

        public static IQueryable<ProjekatListModelItem> ToProjekatListModelItem(this IQueryable<Projekat> query)
        {
            return query.Select(projekat => new ProjekatListModelItem()
            {
                Id=projekat.Id,
                Naziv = projekat.Naziv,
                Opis = projekat.Opis
            });
        
        }

        public static IQueryable<KorisnikListModelItem> ToProjekatKorisniciModelItem(this IQueryable<Entities.Models.Korisnik.KorisnikProjekat> query)
        {
             var result=query.Select(projekat => projekat.KorisnikUloga.Korisnik);
           
            return result.Select(korisnik => new KorisnikListModelItem()
            {
                KorisnickoIme = korisnik.KorisnickoIme,
                Uloga = string.Join(", ", korisnik.KorisnikUloge
                                                  .Select(a => a.Uloga.Naziv)
                                                  .ToList()),
                Email = korisnik.Email,
                PunoIme = korisnik.PunoIme,
                DatumKreiranja = korisnik.DatumKreiranja,
                Onemogucen = korisnik.Onemogucen

            });
        }



    }
}
