using Autofac;
using Web.Core.Auth;
using Web.Core.Auth.Implementation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Core.Registration
{
    /// <summary>
    /// Modul za registraciju projekta u Autofac IoC kontejner
    /// </summary>
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // registrujemo hash provajder
            builder.RegisterType<BcryptHashProvider>().As<IHashProvider>();
        }
    }
}
