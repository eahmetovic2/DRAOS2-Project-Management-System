using Autofac;
using Autofac.Extensions.DependencyInjection;
using Web.Core.Registration;
using Web.Services.Registration;
using Web.UserAgent.Registration;
using Manatee.Trello;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Web.Api.Config
{
    /// <summary>
    /// Postavlja konfiguraciju Autofac IoC kontejnera
    /// </summary>
    public class IocConfig
    {
        /// <summary>
        /// Konfiguracija servisa aplikacije
        /// </summary>
        public static IServiceProvider ConfigureServices(
            IServiceCollection services,
            IConfigurationRoot configuration)
        {
            var builder = new ContainerBuilder();


            // dodaj module aplikacije u kontejner
            builder.RegisterModule<CoreModule>();
            builder.RegisterModule<UserAgentModule>();
            builder.RegisterModule<ServiceModule>();
      

            //dodajem mogucnost resolvanja context-a. koristenou AuthService implementaciji
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //dodajem implementaciju auth service
            builder.RegisterType<Auth.Services.AuthService>().As<Services.IAuthService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            //dodajem implemetaciju izvjestaj service
            builder.RegisterType<Common.Services.IzvjestajService>().As<Services.IIzvjestajService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            //dodajem implemetaciju ApplicationConfigurationService
            builder.RegisterType<Common.Services.ApplicationConfigurationService>().As<Services.IApplicationConfigurationService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

      

            // dodaj asp.net servise u kontejner
            builder.Populate(services);

       
        

            // konstruisi kontejner
            var container = builder.Build();

            // create the IServiceProvider based on the container.
            return new AutofacServiceProvider(container);
        }
    }
}
