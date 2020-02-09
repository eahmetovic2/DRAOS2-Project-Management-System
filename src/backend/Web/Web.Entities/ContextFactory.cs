using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;

namespace Web.Entities
{
    /// <summary>
    /// Klasa koja kreira db kontekst za izvrsenje migracija
    /// </summary>
    public class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        /// <summary>
        /// Kreira db kontekst
        /// </summary>
        /// <returns>Db kontekst</returns>
        public Context CreateDbContext(string[] args)
        {
            // dobavimo postavke aplikacije
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();

            // podesimo bazu
            var connectionString = configuration.GetConnectionString("TMSContext");

            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(connectionString, x => x.UseRowNumberForPaging());

            // kreiramo kontekst
            return new Context(optionsBuilder.Options);
        }
    }
}
