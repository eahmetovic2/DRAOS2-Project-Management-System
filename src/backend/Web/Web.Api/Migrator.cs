using Web.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Web.Api
{
    /// <summary>
    /// Klasa koja radi migraciju baze
    /// </summary>
    public class Migrator
    {
        /// <summary>
        /// Vrsi migraciju baze
        /// </summary>
        /// <param name="contentRootPath">Putanja direktorija gdje se aplikacija izvrsava</param>
        public static void Migrate(String contentRootPath)
        {
            // dobavimo postavke aplikacije
            var builder = new ConfigurationBuilder()
                .SetBasePath(contentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();

            // podesimo bazu
            var connectionString = configuration.GetConnectionString("TMSContext");

            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(connectionString, x => x.UseRowNumberForPaging());

            // uradimo migraciju
            using (var context = new Context(optionsBuilder.Options))
            {
                context.Database.Migrate();
            }
        }
    }
}
