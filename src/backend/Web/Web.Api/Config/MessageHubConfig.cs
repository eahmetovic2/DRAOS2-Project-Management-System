using Easy.MessageHub;
using Web.Entities.Models;
using Web.Entities.Models.Base;
using Web.Services;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Web.Api.Config
{
    public class MessageHubConfig
    {
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
}
