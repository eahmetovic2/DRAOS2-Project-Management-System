using Web.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Config
{
    /// <summary>
    /// Postavlja konfiguraciju za entity framework
    /// </summary>
    public class DataConfig
    {
        /// <summary>
        /// Konfiguracija servisa aplikacije
        /// </summary>
        public static void ConfigureServices(
            IServiceCollection services, 
            IConfigurationRoot configuration)
        {
            // add entity framework
            var connectionString = configuration.GetConnectionString("PMSContext");
            services.AddDbContext<Context>(options => options.UseSqlServer(connectionString, x=> x.UseRowNumberForPaging()));
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
            // seed database
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<Context>();
                context.EnsureSeedData();
            }
        }
    }
}
