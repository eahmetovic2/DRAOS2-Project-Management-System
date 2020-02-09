using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Entities.ContextExtensions
{
    public static class Uloga
    {
        public static void Execute(Context context)
        {
            var entity = context.Uloge.FirstOrDefault(x => x.Sifra == "neodredeno" && !x.IsDeleted);
            if (entity == null)
                context.Uloge.Add(new Models.Sifarnik.Uloga()
                {
                    Naziv = "Neodređeno",
                    Sifra = "neodredeno",
                    VrijednostUAplikaciji = 0,
                    Poredak = 0,
                    IsDeleted = false,
                    DatumKreiranja = DateTime.Now,
                    FrontendModulId = 1
                });

            context.SaveChanges();

            entity = context.Uloge.FirstOrDefault(x => x.Sifra == "administrator" && !x.IsDeleted);
            if (entity == null)
                context.Uloge.Add(new Models.Sifarnik.Uloga()
                {
                    Naziv = "Administrator",
                    Sifra = "administrator",
                    VrijednostUAplikaciji = 1,
                    Poredak = 1,
                    IsDeleted = false,
                    DatumKreiranja = DateTime.Now,
                    FrontendModulId = 1
                });

            context.SaveChanges();

            entity = context.Uloge.FirstOrDefault(x => x.Sifra == "moderator" && !x.IsDeleted);
            if (entity == null)
                context.Uloge.Add(new Models.Sifarnik.Uloga()
                {
                    Naziv = "Moderator",
                    Sifra = "moderator",
                    VrijednostUAplikaciji = 2,
                    Poredak = 2,
                    IsDeleted = false,
                    DatumKreiranja = DateTime.Now,
                    FrontendModulId = 1
                });


            context.SaveChanges();

            entity = context.Uloge.FirstOrDefault(x => x.Sifra == "support" && !x.IsDeleted);
            if (entity == null)
                context.Uloge.Add(new Models.Sifarnik.Uloga()
                {
                    Naziv = "Support",
                    Sifra = "support",
                    VrijednostUAplikaciji = 3,
                    Poredak = 3,
                    IsDeleted = false,
                    DatumKreiranja = DateTime.Now,
                    FrontendModulId = 1
                });

            context.SaveChanges();


            entity = context.Uloge.FirstOrDefault(x => x.Sifra == "taskUser" && !x.IsDeleted);
            if (entity == null)
                context.Uloge.Add(new Models.Sifarnik.Uloga()
                {
                    Naziv = "Task user",
                    Sifra = "taskUser",
                    VrijednostUAplikaciji = 4,
                    Poredak = 4,
                    IsDeleted = false,
                    DatumKreiranja = DateTime.Now,
                    FrontendModulId = 1
                });

            context.SaveChanges();

        }
    }
}
