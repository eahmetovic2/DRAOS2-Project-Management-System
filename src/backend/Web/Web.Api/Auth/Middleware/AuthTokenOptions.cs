using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Auth.Middleware
{
    /// <summary>
    /// Opcije za custom auth middleware
    /// </summary>
    public class AuthTokenOptions : AuthenticationSchemeOptions
    {
        /// <summary>
        /// Prazan konstruktor
        /// </summary>
        public AuthTokenOptions()
        {
            this.AuthenticationScheme = "EmisScheme";
            this.AutomaticAuthenticate = true;
            this.AutomaticChallenge = true;
        }

        public string AuthenticationScheme { get; }
        public bool AutomaticAuthenticate { get; }
        public bool AutomaticChallenge { get; }
    }
}
