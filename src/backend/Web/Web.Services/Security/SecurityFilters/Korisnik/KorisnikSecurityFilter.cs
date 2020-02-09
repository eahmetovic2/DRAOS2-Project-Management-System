using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Core.Database;
using Web.Entities;

namespace Web.Services.Security.SecurityFilters.Korisnik
{
    internal class KorisnikSecurityFilter : SecurityFilter<Entities.Models.Korisnik.Korisnik>
    {
        private IAuthService authService;
        private IKorisnikService korisnikService;
        private Context context;

        public KorisnikSecurityFilter(IAuthService authService, IKorisnikService korisnikService, Context context)
        {
            this.authService = authService;
            this.korisnikService = korisnikService;
            this.context = context;
        }

        public override IQueryable<Entities.Models.Korisnik.Korisnik> Secure(IQueryable<Entities.Models.Korisnik.Korisnik> query, SecurityLevel securityLevel)
        {
            var korisnik = authService.TrenutniKorisnik();

            if (korisnik == null)
                return query;
            
            return query;
        }
    }
}
