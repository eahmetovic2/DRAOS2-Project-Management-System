using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities;
using Web.Entities.Models.Projekat;
using Web.Models.Mapping.Mappers.Base.ProjekatMap;
using Web.Models.Request.Projekat.ZahtjevPrioritet;
using Web.Models.Response.Projekat.ZahtjevPrioritet;
using Web.Services.Definition.Projekat;
using Web.Services.Result;

namespace Web.Services.Implementation.Base
{
    
        public class ZahtjevPrioritetService : Service, IZahtjevPrioritetService
        {

            /// <summary>
            /// Entity framework db kontekst 
            /// </summary>
            private Context context;


            private IAuthService authService;

            /// <summary>
            /// 
            /// </summary>


            private IApplicationConfigurationService applicationConfigurationService;


            /// <summary>
            /// Konstruktor servisa
            /// </summary>
            public ZahtjevPrioritetService(ILifetimeScope scope, Context context, IAuthService authService, IApplicationConfigurationService applicationConfigurationService)
                : base(scope)
            {
                this.context = context;
                this.authService = authService;
                this.applicationConfigurationService = applicationConfigurationService;

            }


            public ServiceResult<List<ZahtjevPrioritetModel>> VratiSveZahtjevePrioritetaProjekta(int projekatId)
            {
                var zahtjevPrioriteti = context.ZahtjevPrioriteti
                    .Where(p => p.ProjekatId == projekatId).OrderBy(p => p.Poredak).ToZahtjevPrioritetModel().ToList();
                if (zahtjevPrioriteti == null)
                    return NotFound();

                return Ok(zahtjevPrioriteti);
            }

        public ServiceResult<List<ZahtjevPrioritet>> DodajNoviPrioritetZahtjevaProjekta(int projekatId, KreirajZahtjevPrioritetRequestModel zahtjevpPrioritetModel)
        {
            if (zahtjevpPrioritetModel.Naziv.Length > 20)
                return Error("Naziv ne može biti veći od 20 karaktera");

            var zahtjevPrioriteti = context.ZahtjevPrioriteti
              .Where(p => p.ProjekatId == projekatId).OrderBy(p => p.Poredak).ToList();

            if (zahtjevPrioriteti == null)
                return NotFound();

            //ako npr. dodan prioritet sa poredak 2, onda se poredak za ostale prioritete tog projekta povecava za 1 ako je taj poredak veci ili jednak od dodanog novog poretka
            for (int brojPrioriteta = zahtjevpPrioritetModel.Poredak; brojPrioriteta < zahtjevPrioriteti.Count(); brojPrioriteta++)
            {
                zahtjevPrioriteti.ElementAt(brojPrioriteta).Poredak = brojPrioriteta + 1;
            }


            ZahtjevPrioritet zahtjevPrioritet = new ZahtjevPrioritet();
            zahtjevPrioritet.Default = false;
            zahtjevPrioritet.Naziv = zahtjevpPrioritetModel.Naziv;
            zahtjevPrioritet.Poredak = zahtjevpPrioritetModel.Poredak;
            zahtjevPrioritet.ProjekatId = projekatId;

            context.Add(zahtjevPrioritet);

            SaveChanges(context);

            zahtjevPrioriteti = context.ZahtjevPrioriteti
              .Where(p => p.ProjekatId == projekatId).OrderBy(p => p.Poredak).ToList();

            return Ok(zahtjevPrioriteti);
        }

        public ServiceResult<Nothing> AzurirajDefaultniZahtjevPrioritetProjekta(int projekatId, AzurirajDefaultniZahtjevPrioritetProjektaRequestModel zahtjevpPrioritetModel)
        {

            var zahtjevPrioriteti = context.ZahtjevPrioriteti
              .Where(p => p.ProjekatId == projekatId).ToList();

            foreach(var z in zahtjevPrioriteti)
            {
                if (z.Id == zahtjevpPrioritetModel.Id)
                {
                    z.Default = true;
                }
                else z.Default = false;
            }

            SaveChanges(context);

            return Ok();
        }
    }
    

}
