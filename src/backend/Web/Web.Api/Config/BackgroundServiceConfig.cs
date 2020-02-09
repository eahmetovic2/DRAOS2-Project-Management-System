using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Web.Api.Config
{
    /// <summary>
    /// Postavlja konfiguraciju za background procesiranje
    /// </summary>
    public class BackgroundServiceConfig
    {
        /// <summary>
        /// Konfiguracija servisa aplikacije
        /// </summary>
        public static void ConfigureServices(
            IServiceCollection services,
            IConfigurationRoot configuration)
        {
            services.AddHangfire(c => c.UseMemoryStorage());
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
            app.UseHangfireServer();
        }
    }
}
