using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Auth.Requirements
{
    /// <summary>
    /// Rezultat provjere
    /// </summary>
    public enum CheckResult
    {
        /// <summary>
        /// Zahtjev je zadovoljen
        /// </summary>
        Success,

        /// <summary>
        /// Zahtjev je eksplicitno odbijen
        /// </summary>
        Failure,

        /// <summary>
        /// Zahtjev nije ni zadovoljen ni odbijen
        /// </summary>
        Ignore
    }
}
