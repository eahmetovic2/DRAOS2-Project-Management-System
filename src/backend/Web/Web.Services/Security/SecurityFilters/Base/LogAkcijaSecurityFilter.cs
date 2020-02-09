using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Core.Database;

namespace Web.Services.Security.SecurityFilters.Base
{
    internal class LogAkcijaSecurityFilter : SecurityFilter<Entities.Models.Base.LogAkcija>
    {
        private IAuthService authService;

        public LogAkcijaSecurityFilter(IAuthService authService)
        {
            this.authService = authService;
        }

        public override IQueryable<Entities.Models.Base.LogAkcija> Secure(IQueryable<Entities.Models.Base.LogAkcija> query, SecurityLevel securityLevel)
        {
            return query;
        }
    }
}
