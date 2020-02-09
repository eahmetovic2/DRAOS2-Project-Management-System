using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Web.Entities.Models;

namespace Web.Entities
{
    /// <summary>
    /// Ekstenzije za data context klasu
    /// </summary>
    public static class DBContextExtensions
    {
        /// <summary>
        /// Kreiraj pocetne podatke ako je baza prazna
        /// </summary>
        /// <param name="context">Db context</param>
        public static void EnsureSeedData(this Context context)
        {
            // da li postoje migracije koje nisu izvrene
            //if (context.Database.GetPendingMigrations().Any())
            //    return;

            ContextExtensions.FrontendModul.Execute(context);

            ContextExtensions.Uloga.Execute(context);


            ContextExtensions.AdminKorisnik.Execute(context);
     
            
            ContextExtensions.LogKategorija.Execute(context);
            ContextExtensions.LogLevel.Execute(context);
          
          
           
        }
    }
}
