using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Core.Database;
using Web.Entities;
using Web.Entities.Models.Sifarnik;

namespace Web.Services.Security.SecurityFilters.Korisnik
{
    internal class UlogaSecurityFilter : SecurityFilter<Uloga>
    {
        private IAuthService authService;
        private Context context;

        public UlogaSecurityFilter(IAuthService authService, Context context)
        {
            this.authService = authService;
            this.context = context;
        }

        public override IQueryable<Uloga> Secure(IQueryable<Uloga> query, SecurityLevel securityLevel)
        {
            return query;
        }
    }
}
