using Web.Api.Auth.Middleware;
using Web.Api.Auth.Requirements;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Web.Api.Config
{
    /// <summary>
    /// Postavlja konfiguraciju za authentikaciju i autorizaciju
    /// </summary>
    public static class AuthConfig
    {
        /// <summary>
        /// Konfiguracija servisa aplikacije
        /// </summary>
        public static void ConfigureServices(
            IServiceCollection services,
            IConfigurationRoot configuration)
        {
            // podesavamo police za autorizaciju
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "EmisScheme";
                options.DefaultAuthenticateScheme = "EmisScheme";
                options.DefaultChallengeScheme = "EmisScheme";
            })
            .AddCustomAuthentication(o => { });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserIsAdmin", policy =>
                    policy.AddAnyChecks(new RoleChecker("Administrator")));
                options.AddPolicy("UserIsOwner", policy =>
                    policy.AddAnyChecks(new UserIsOwnerChecker()));
                options.AddPolicy("UserIsAdminOrOwner", policy =>
                    policy.AddAnyChecks(new UserIsOwnerChecker(), new RoleChecker("Administrator")));
                options.AddPolicy("UserIsUpravaSkole", policy =>
                    policy.AddAnyChecks(new RoleChecker("UpravaSkole")));
                options.AddPolicy("UserIsAdminOrUpravaSkole", policy =>
                    policy.AddAnyChecks(new RoleChecker("UpravaSkole"), new RoleChecker("Administrator")));
                options.AddPolicy("UserIsAdminOrMinistarstvo", policy =>
                    policy.AddAnyChecks(new RoleChecker("Administrator"), new RoleChecker("Ministarstvo")));
            });

        }

        /// <summary>
        /// Konfiguracija aplikacije
        /// </summary>
        public static void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IConfigurationRoot configuration)
        {
            // koristimo custom authentikaciju
            app.UseAuthentication();
        }

        static AuthenticationBuilder AddCustomAuthentication(this AuthenticationBuilder builder, Action<AuthTokenOptions> configureOptions)
        {
            return builder.AddScheme<AuthTokenOptions, AuthTokenHandler>("EmisScheme", "EmisScheme", configureOptions);
        }
    }
}