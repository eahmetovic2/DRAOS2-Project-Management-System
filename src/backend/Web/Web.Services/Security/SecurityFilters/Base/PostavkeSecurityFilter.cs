using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Core.Database;
using Web.Entities.Models.Base;

namespace Web.Services.Security.SecurityFilters.Base
{
    internal class PostavkeSecurityFilter : SecurityFilter<Postavke>
    {
        private IAuthService authService;

        public PostavkeSecurityFilter(IAuthService authService)
        {
            this.authService = authService;
        }

        public override IQueryable<Postavke> Secure(IQueryable<Postavke> query, SecurityLevel securityLevel)
        {          

            return query;
        }
    }
}
