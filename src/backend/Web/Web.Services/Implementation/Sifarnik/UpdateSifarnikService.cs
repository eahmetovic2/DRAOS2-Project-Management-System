using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Core.Constants;
using Web.Core.Database;
using Web.Entities;
using Web.Models.Request.Sifarnik;
using Web.Services.Definition.Sifarnik;

namespace Web.Services.Implementation.Sifarnik
{
    /// <summary>
    /// Servis koji vraca dictionary za izmjenu sifarnika
    /// </summary>
    public class UpdateSifarnikService : Service, IUpdateSifarnikService
    {
        private ISnimanjeIzmjenaPomocnoService snimanjeIzmjenaPomocnoService;

        public UpdateSifarnikService(ILifetimeScope scope, ISnimanjeIzmjenaPomocnoService snimanjeIzmjenaPomocnoService) : base(scope)
        {
            this.snimanjeIzmjenaPomocnoService = snimanjeIzmjenaPomocnoService;
        }

        public Dictionary<ESifarnik, Func<Context, UpdateSifarnikRequestModel, ILifetimeScope, bool>> GetUpdateSifarnici()
        {
            return new
        Dictionary<ESifarnik, Func<Context, UpdateSifarnikRequestModel, ILifetimeScope, bool>>() {
            {
                ESifarnik.Pol,
                new Func<Context, UpdateSifarnikRequestModel, ILifetimeScope, bool>
                (
                    (context, model, scope) => {

                        var entity = Secure (context.Polovi, new SecurityLevel { Update = true }, model.Scope).First (a => a.Id == model.Id);
                        return snimanjeIzmjenaPomocnoService.UpdateEntity (context, model, entity, context.Polovi, scope);
                    }
                )
            }
        };
        }
    }
}
