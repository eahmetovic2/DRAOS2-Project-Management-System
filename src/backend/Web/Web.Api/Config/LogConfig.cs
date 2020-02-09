using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Config
{
    /// <summary>
    /// Podesava logiranje u aplikaciji
    /// </summary>
    public class LogConfig
    {
        /// <summary>
        /// Konfiguracija servisa aplikacije
        /// </summary>
        public static void ConfigureServices(
            IServiceCollection services,
            IConfigurationRoot configuration)
        {
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
            var loggingConfig = configuration.GetSection("Logging");

            loggerFactory.AddConsole(loggingConfig);
            loggerFactory.AddDebug();
        }
    }
}
