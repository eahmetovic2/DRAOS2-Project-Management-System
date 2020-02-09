using Web.Entities.Models;
using Web.Entities.Models.Korisnik;
using System;
using System.Linq;
using System.Text;
using Web.Core.Constants;

namespace Web.Entities.ContextExtensions
{
    public static class AdminKorisnik
    {
        public static void Execute(Context context)
        {
            //todo ovo je potrebno ponovo implementirati sa dinamickim ulogama



            // lozinka adminpass
            var tajna = Encoding.UTF8.GetBytes("$2b$10$/cyJgky4qQQdjw93lfDVWOehzy1E1OP3a998cnnVuv2/C2korT1pq");

            if (!context.Korisnici.Any())
            {
                context.Korisnici.Add(new Korisnik()
                {
                    KorisnickoIme = "admin",
                    Email = "admin@example.ba",
                    PunoIme = "Administrator",
                    Tajna = tajna,
                    DatumKreiranja = DateTime.Now,
                    Onemogucen = false,
                    Jezik = (int)Jezici.bs
                });

                context.SaveChanges();

                var ulogaId = context.Uloge.Where(x => x.Sifra == "administrator").FirstOrDefault().Id;

                context.KorisnikUloge.Add(new KorisnikUloga
                {
                    UlogaId = ulogaId,
                    KorisnickoIme = "admin"
                });
            }
        }
    }
}
