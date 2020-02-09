using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Core.Database;

namespace Web.Services.Security.SecurityFilters.Sifarnik
{
    internal class SifarnkSecurityFilter : SecurityFilter<Entities.Models.Sifarnik.Sifarnik>
    {
        private IAuthService authService;

        public SifarnkSecurityFilter(IAuthService authService)
        {
            this.authService = authService;
        }

        public override IQueryable<Entities.Models.Sifarnik.Sifarnik> Secure(IQueryable<Entities.Models.Sifarnik.Sifarnik> query, SecurityLevel securityLevel)
        {

            return query;
        }
    }
}