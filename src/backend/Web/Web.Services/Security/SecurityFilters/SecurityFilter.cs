using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Core.Database;

namespace Web.Services.Security.SecurityFilters
{
    public abstract class SecurityFilter<T> : ISecurityFilter<T>
    {
        public abstract IQueryable<T> Secure(IQueryable<T> query, SecurityLevel securityLevel);

        public IQueryable<T> VratiPrazan(IQueryable<T> query)
        {
            return Enumerable.Empty<T>().AsQueryable();
        }
    }
}
