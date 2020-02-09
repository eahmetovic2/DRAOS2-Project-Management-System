using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Auth.Requirements
{
    /// <summary>
    /// Genericki auth zahtjev koji dozvoljava kombinovanje pravila za
    /// razliku od standardnih asp.net requirement-a koji nemaju dovoljnu fleksibilnost:
    /// https://github.com/aspnet/Security/issues/564
    /// </summary>
    public class CheckingRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Lista provjera za jedan request
        /// </summary>
        public IEnumerable<IChecker> Checkers { get; private set; }

        /// <summary>
        /// Konstruktor koji prima listu
        /// </summary>
        /// <param name="checkers">Lista provjera</param>
        public CheckingRequirement(IEnumerable<IChecker> checkers)
        {
            this.Checkers = checkers;
        }

        /// <summary>
        /// Konstruktor koji prima params niz
        /// </summary>
        /// <param name="checkers">niz provjera</param>
        public CheckingRequirement(params IChecker[] checkers)
        {
            this.Checkers = checkers;
        }
    }

    /// <summary>
    /// Handler za genericki auth zahtjev iznad
    /// </summary>
    public class CheckingHandler : AuthorizationHandler<CheckingRequirement>
    {
        /// <summary>
        /// Radi provjeru requirement-a
        /// </summary>
        /// <param name="context">Kontekst provjere</param>
        /// <param name="requirement">Zahtjev koji se provjerava</param>
        /// <returns>Da li je zahtjev ispunjen, odbijen ili preskocen</returns>
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CheckingRequirement requirement)
        {
            // izvrsi sve provjere
            var results = requirement.Checkers.Select(c =>
                c.Check(context, c))
                .ToList();

            // provjeri rezultate provjera
            // ako je jedna od provjera vratila fail, uradi fail,
            // ako je jedna od provjera vratila success, uradi succeed
            if (results.Any(r => r == CheckResult.Failure))
                context.Fail();
            else if (results.Any(r => r == CheckResult.Success))
                context.Succeed(requirement);

            // inace je zahtjev preskocen
            return Task.CompletedTask;
        }
    }
}
