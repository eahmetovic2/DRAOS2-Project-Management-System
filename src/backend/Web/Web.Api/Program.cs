using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Web.Api
{
    /// <summary>
    /// Glavna klasa aplikacije
    /// </summary>
    public class Program
    {

        public static void Main(string[] args)
        {
            var rootPath = Directory.GetCurrentDirectory();

            // provjeri argumente aplikacije
            if (args.Length > 0)
            {
                switch (args[0].ToLower())
                {
                    case "migrate":
                        // koristena opcija migrate, uradi migraciju baze i ugasi aplikaciju
                        Migrator.Migrate(rootPath);
                        return;
                }
            }

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

