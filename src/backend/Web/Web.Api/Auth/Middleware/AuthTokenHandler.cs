using Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Models.Response.Token;
using Web.UserAgent;
using Web.Api.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using Autofac.Core.Lifetime;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;

namespace Web.Api.Auth.Middleware
{
    /// <summary>
    /// Handler za custom authentikaciju
    /// </summary>
    public class AuthTokenHandler : AuthenticationHandler<AuthTokenOptions>
    {
        /// <summary>
        /// Ime token header-a koji trazimo
        /// </summary>
        private static String authTokenName = "X-Auth-Token";
        private static String authTempTokenName = "ttxid";

        /// <summary>
        /// IoC kontejner
        /// </summary>
        private IServiceProvider serviceProvicer;

        /// <summary>
        /// Konstruktor handlera
        /// </summary>
        /// <param name="serviceProvicer">IoC kontejner</param>
        public AuthTokenHandler(IServiceProvider serviceProvicer, IOptionsMonitor<AuthTokenOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
            this.serviceProvicer = serviceProvicer;
        }

        /// <summary>
        /// Uradi provjeru authentikacije
        /// </summary>
        /// <returns>Rezultat provjere authentikacije</returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // da li header koji nas zanima postoji
            var headers = Request.Headers;
            Microsoft.Extensions.Primitives.StringValues tokenStringValues;
            Guid tokenId;
            String tokenString;

            /*ako je provjera temp tokena*/
            if (Request.QueryString.Value.Contains(authTempTokenName))
            {
                if (!Request.Query.TryGetValue(authTempTokenName, out tokenStringValues))
                    return await Task.FromResult(AuthenticateResult.NoResult());
                tokenString = tokenStringValues.ToString();

                if (!Guid.TryParse(tokenString, out tokenId))
                    return await Task.FromResult(AuthenticateResult.NoResult());
                // provjeri header
                return await Task.FromResult(ValidateToken(tokenId));
            }

            if (!headers.ContainsKey(authTokenName))
                return await Task.FromResult(AuthenticateResult.NoResult());

            // dobavi vrijednost headera kao Guid
            tokenString = headers[authTokenName].FirstOrDefault();
            if (!Guid.TryParse(tokenString, out tokenId))
                return await Task.FromResult(AuthenticateResult.NoResult());

            // provjeri header
            return await Task.FromResult(ValidateToken(tokenId));
        }

        /// <summary>
        /// Uradi validaciju tokena
        /// </summary>
        /// <param name="tokenId">Token koji treba validirati</param>
        /// <returns>Rezultat validacije</returns>
        private AuthenticateResult ValidateToken(Guid tokenId)
        {
            // u ovom dijeli procesiranja requesta jos nije kreiran lifetime za request
            // ovo je mozda bug asp.net-a, ali mozemo jednostavno napraviti novi lifetime
            // ako to ne uradimo, onda ce ef context za svaki request biti identican
            var lifetimeScope = serviceProvicer.GetService<ILifetimeScope>();
            using (var subScope = lifetimeScope.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag))
            {
                // dobavimo servise
                var tokenService = subScope.Resolve<ITokenService>();
                var userAgentParser = subScope.Resolve<IUserAgentParser>();

                // provjerimo da li je token validan
                var ip = Request.GetIpAddress();
                var klijent = Request.ParseUserAgent(userAgentParser);
                var token = tokenService.ValidirajToken(tokenId, ip, klijent);
                if (token.IsOk && token.Value.Tip == Core.Constants.TokenTip.Temp)
                {
                    tokenService.ObrisiToken(token.Value.Vlasnik.KorisnickoIme, token.Value.Id);

                }

                // ako nije validan vratimo rezultat
                if (!token.IsOk)
                    return AuthenticateResult.NoResult();

                // inace postavimo korisnika
                return HandleAuthorized(token.Value);
            }
        }

        /// <summary>
        /// Token je validan, postavi korisnika
        /// </summary>
        /// <param name="value">Model validnog tokena</param>
        /// <returns>Rezultat uspjesne authentikacije</returns>
        private AuthenticateResult HandleAuthorized(TokenModel value)
        {
            // kreiraj claim za ime i rolu korisnika
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, value.Vlasnik.KorisnickoIme, ClaimValueTypes.String));
            claims.Add(new Claim(ClaimTypes.Role, value.Vlasnik.TrenutnaUlogaId.ToString(), ClaimValueTypes.String));

            foreach (var dozvoljenaAkcija in value.Vlasnik.DozvoljeneAkcije.Items.Select(a => a.Sifra))
            {
                claims.Add(new Claim(ClaimTypes.UserData, dozvoljenaAkcija));
            }

            // kreiraj principal sa claimovima
            var identity = new ClaimsIdentity(claims, Options.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // kreiraj i vrati ticket za kreirani principal
            var ticket = new AuthenticationTicket(principal, new Microsoft.AspNetCore.Authentication.AuthenticationProperties(), Options.AuthenticationScheme);
            return AuthenticateResult.Success(ticket);
        }
    }
}