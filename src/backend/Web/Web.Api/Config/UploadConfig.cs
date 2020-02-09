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
    public class UploadConfig
    {
        /// <summary>
        /// Konfiguracija servisa aplikacije
        /// </summary>
        public static void ConfigureServices(
            IServiceCollection services,
            IConfigurationRoot configuration)
        {          
            services.Configure<UploadSettings>(configuration.GetSection("Upload"));
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
        }
    }

    public class UploadSettings
    {
        public string PutanjaFoldera { get; set; }
        public string DozvoljeneEkstenzije { get; set; }
        public int MaksimalnaVelicinaMB { get; set; }
    }
}
