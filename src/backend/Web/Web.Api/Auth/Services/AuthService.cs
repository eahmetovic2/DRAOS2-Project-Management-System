using Autofac;
using Web.Entities;
using Web.Entities.Models.Korisnik;
using Web.Services;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace Web.Api.Auth.Services
{
    public class AuthService : IAuthService
    {
        /// <summary>
        /// Entity framework db kontekst 
        /// </summary>
        private Context context;

        /// <summary>
        /// Dozvoljava pristup Contex-u trenutnog request-a
        /// </summary>
        private IHttpContextAccessor accessor;

        private Korisnik trenutni;

        /// <summary>
        /// Konstruktor servisa
        /// </summary>
        public AuthService(ILifetimeScope scope, IHttpContextAccessor accessor)

        {
            this.context = scope.Resolve<Context>();
            this.accessor = accessor;
            trenutni = DajTrenutnogKorisnikaIzBaze();
        }

        public Korisnik TrenutniKorisnik()
        {
            return trenutni;
        }

        private Korisnik DajTrenutnogKorisnikaIzBaze()
        {
            if (accessor.HttpContext != null)
            {
                var korisnickoIme = accessor.HttpContext.User.Identity.Name;
                var korisnik = context.Korisnici
                              .FirstOrDefault(x => x.KorisnickoIme == korisnickoIme);

                var ulogaClaim = accessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == System.Security.Claims.ClaimTypes.Role);

                if (ulogaClaim != null)
                {
                    korisnik.TrenutnaUlogaId = Convert.ToInt32(ulogaClaim.Value);

                    korisnik.TrenutnaUloga = context.Uloge.FirstOrDefault(a => a.Id == korisnik.TrenutnaUlogaId);

                    // ucitaj samo one korisnik uloge koje trebaju
                    context.Entry(korisnik.TrenutnaUloga).Collection(a => a.KorisnikUloge)
                                                         .Query()
                                                         .Where(a => a.KorisnickoIme == korisnik.KorisnickoIme)
                                                         .Load();
                }

                return korisnik;
            }
            return null;
        }
    }
}
