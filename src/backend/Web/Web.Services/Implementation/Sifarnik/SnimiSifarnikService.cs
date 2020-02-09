using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Core.Constants;
using Web.Entities;
using Web.Entities.Models.Sifarnik;
using Web.Models.Request.Sifarnik;
using Web.Services.Definition.Sifarnik;
using Web.Services.Implementation;

namespace Web.Services.Implementation.Sifarnik
{
    /// <summary>
    /// Servis koji vraca dictionary za snimanje sifarnika
    /// </summary>
    public class SnimiSifarnikService : Service, ISnimiSifarnikService
    {
        private ISnimanjeIzmjenaPomocnoService snimanjeIzmjenaPomocnoService;

        public SnimiSifarnikService(ILifetimeScope scope, ISnimanjeIzmjenaPomocnoService snimanjeIzmjenaPomocnoService) : base(scope)
        {
            this.snimanjeIzmjenaPomocnoService = snimanjeIzmjenaPomocnoService;
        }

        public Dictionary<ESifarnik, Func<Context, KreirajSifarnikRequestModel, ILifetimeScope, bool>> GetSnimiSifarnik()
        {
            return new
            Dictionary<ESifarnik, Func<Context, KreirajSifarnikRequestModel, ILifetimeScope, bool>>() {
                {
                    ESifarnik.Pol,
                    new Func<Context, KreirajSifarnikRequestModel, ILifetimeScope, bool>
                    (
                        (context, model, scope) => {
                            var entity = new Pol ();
                            return snimanjeIzmjenaPomocnoService.SaveEntity<Pol> (context, model, entity, context.Polovi, scope);
                        }
                    )
                }
            };
        }
    }
}