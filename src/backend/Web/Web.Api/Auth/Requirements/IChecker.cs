using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Auth.Requirements
{
    /// <summary>
    /// Interfejs auth provjere 
    /// </summary>
    public interface IChecker
    {
        /// <summary>
        /// Uradi provjeru konteksta
        /// </summary>
        /// <param name="context">Kontekst auth provjere</param>
        /// <param name="self"></param>
        CheckResult Check(AuthorizationHandlerContext context, IChecker self);
    }
}
