using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Web.Api.Config;
using Web.Entities;

namespace Web.Api
{
    /// <summary>
    /// Startup klasa za konfiguraciju servera
    /// </summary>
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = configuration.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {           
            BackgroundServiceConfig.ConfigureServices(services, Configuration);
            MvcConfig.ConfigureServices(services, Configuration);
          
            AuthConfig.ConfigureServices(services, Configuration);
            DataConfig.ConfigureServices(services, Configuration);
            CacheConfig.ConfigureServices(services, Configuration);
           
            ReportServiceConfig.ConfigureServices(services, Configuration);
            UploadConfig.ConfigureServices(services, Configuration);

            return IocConfig.ConfigureServices(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //TODO:
            //if (env.IsDevelopment())
            //    app.UseDeveloperExceptionPage();
            
            BackgroundServiceConfig.Configure(app, env, loggerFactory, Configuration);
            DataConfig.Configure(app, env, loggerFactory, Configuration);
            LogConfig.Configure(app, env, loggerFactory, Configuration);
            AuthConfig.Configure(app, env, loggerFactory, Configuration);
            MvcConfig.Configure(app, env, loggerFactory, Configuration);
           
            ReportServiceConfig.Configure(app, env, loggerFactory, Configuration);
			MessageHubConfig.Configure(app, env, loggerFactory, Configuration);                   
        }
    }
}
