using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Web.Api.Auth.Requirements
{
    /// <summary>
    /// Provjera koja provjerava da li je korisnik dio zadate role
    /// </summary>
    public class RoleChecker : IChecker
    {
        /// <summary>
        /// Rola kojoj korisnik treba da pripada
        /// </summary>
        public String RoleName { get; private set; }

        /// <summary>
        /// Konstruktor provjere
        /// </summary>
        /// <param name="roleName">Ime role</param>
        public RoleChecker(String roleName)
        {
            this.RoleName = roleName;
        }

        /// <summary>
        /// Provjera role korisnika
        /// </summary>
        public CheckResult Check(AuthorizationHandlerContext context, IChecker self)
        {
            if (context.User.IsInRole(RoleName))
                return CheckResult.Success;
            return CheckResult.Ignore;
        }
    }
}
