using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Entities.ContextExtensions
{
    public static class FrontendModul
    {
        public static void Execute(Context context)
        {
            var entity = context.FrontendModuli.FirstOrDefault(x => x.Sifra == "base");
            if (entity == null)
                context.FrontendModuli.Add(new Models.Sifarnik.FrontendModul()
                {
                    Naziv = "Base",
                    Sifra = "base",
                    Onemogucen = false,
                    DatumKreiranja = DateTime.Now
                });

            context.SaveChanges();
            

			context.SaveChanges();
		}
    }
}
