using Autofac;
using Web.UserAgent.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.UserAgent.Registration
{
    public class UserAgentModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var defaultParser = Parser.GetDefault();
            builder.RegisterInstance<Parser>(defaultParser)
                .As<IUserAgentParser>();
        }
    }
}
