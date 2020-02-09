using Autofac;
using Web.Entities.Models;
using Web.Models.Mapping;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Web.Services.Registration
{
    /// <summary>
    /// Modul za registraciju projekta u Autofac IoC kontejner
    /// </summary>
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // registruj svaki tip koji zavrsava sa stringom "Service"
            // kao interfejse koje implementira i pokusaj da koristis
            // lifetime koji ce kreirati jedan servis za jedan request
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(ThisAssembly)
              .Where(t => t.Name.EndsWith("SecurityFilter"))
              .AsImplementedInterfaces()
              .InstancePerLifetimeScope();
        }
    }
}
