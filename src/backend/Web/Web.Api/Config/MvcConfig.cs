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
    /// Podesava asp.net mvc framework
    /// </summary>
    public class MvcConfig
    {
        /// <summary>
        /// Konfiguracija servisa aplikacije
        /// </summary>
        public static void ConfigureServices(
            IServiceCollection services,
            IConfigurationRoot configuration)
        {
            // add framework services.
            services.AddMvc();

            // add cors policies
            services.AddCors(options => {
                options.AddPolicy("AllowAny", builder =>
                    builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                );
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
            // enable cors
            app.UseCors("AllowAny");

            // enable mvc
            app.UseMvc();
        }
    }
}
