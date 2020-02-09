using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Auth.Requirements
{
    /// <summary>
    /// Ekstensije za konstrukciju provjera
    /// </summary>
    public static class PolicyBuilderCheckingExtensions
    {
        /// <summary>
        /// Dodaj provjere kao skup OR provjera
        /// </summary>
        /// <param name="builder">Kreator auth police</param>
        /// <param name="checks">Provjere koje se dodaju</param>
        /// <returns>Kreator auth police</returns>
        public static AuthorizationPolicyBuilder AddAnyChecks(this AuthorizationPolicyBuilder builder, params IChecker[] checks)
        {
            var requirement = new CheckingRequirement(checks);
            builder.Requirements.Add(requirement);
            return builder;
        }

        /// <summary>
        /// Dodaj provjere kao skup AND provjera
        /// </summary>
        /// <param name="builder">Kreator auth police</param>
        /// <param name="checks">Provjere koje se dodaju</param>
        /// <returns>Kreator auth police</returns>
        public static AuthorizationPolicyBuilder AddAllChecks(this AuthorizationPolicyBuilder builder, params IChecker[] checks)
        {
            var requirements = checks.Select(c => new CheckingRequirement(c)).ToArray();
            builder.AddRequirements(requirements);
            return builder;
        }
    }
}
