using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Core.Database;

namespace Web.Services.Security.SecurityFilters.Base
{
    internal class LogEntitetSecurityFilter : SecurityFilter<Entities.Models.Base.LogEntitet>
    {
        private IAuthService authService;

        public LogEntitetSecurityFilter(IAuthService authService)
        {
            this.authService = authService;
        }

        public override IQueryable<Entities.Models.Base.LogEntitet> Secure(IQueryable<Entities.Models.Base.LogEntitet> query, SecurityLevel securityLevel)
        {
            return query;
        }
    }
}
