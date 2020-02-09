using Autofac;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Entities;
using Web.Models.Request.Sifarnik;

namespace Web.Services.Definition.Sifarnik
{
    public interface ISnimanjeIzmjenaPomocnoService : IService
    {
        bool SaveEntity<TEntity>(Context context, KreirajSifarnikRequestModel model, TEntity entity, DbSet<TEntity> entities, ILifetimeScope scope) where TEntity : class;
        bool UpdateEntity<TEntity>(Context context, UpdateSifarnikRequestModel model, TEntity entity, DbSet<TEntity> entities, ILifetimeScope scope) where TEntity : class;
    }
}
