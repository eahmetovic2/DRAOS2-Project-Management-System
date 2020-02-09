using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Core.Constants;
using Web.Entities;
using Web.Models.Request.Sifarnik;

namespace Web.Services.Definition.Sifarnik
{
    public interface ISnimiSifarnikService : IService
    {
        Dictionary<ESifarnik, Func<Context, KreirajSifarnikRequestModel, ILifetimeScope, bool>> GetSnimiSifarnik();
    }
}
