using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities;
using Web.Entities.Models.Projekat;
using Web.Models.Mapping.Mappers.Base.ProjekatMap;
using Web.Models.Request.Projekat.DioProjekta;
using Web.Models.Response.Projekat.DioProjekta;
using Web.Services.Definition.Projekat;
using Web.Services.Result;

namespace Web.Services.Implementation.Base
{
    public class DioProjektaService:Service,IDioProjektaService
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
            public DioProjektaService(ILifetimeScope scope, Context context, IAuthService authService, IApplicationConfigurationService applicationConfigurationService)
                : base(scope)
            {
                this.context = context;
                this.authService = authService;
                this.applicationConfigurationService = applicationConfigurationService;

            }

            public ServiceResult<List<DioProjektaModel>> VratiSveDijeloveProjekta(int projekatId)
            {

                var dijeloviProjekta = context.DijeloviProjekta
                    .Where(p => p.ProjekatId == projekatId).ToDioProjektaModel().ToList();
                if (dijeloviProjekta == null)
                    return NotFound();

                return Ok(dijeloviProjekta);
            }




        public ServiceResult<DioProjekta> DodajNoviDioProjekta(int projekatId, KreirajDioProjektaRequestModel dioProjektaModel)
        {
            if (dioProjektaModel.Naziv.Length > 32)
                return Error("Naziv ne može biti veći od 32 karaktera");

            DioProjekta dioProjekta = new DioProjekta();
            dioProjekta.Naziv = dioProjektaModel.Naziv;
            dioProjekta.ProjekatId = projekatId;

            context.Add(dioProjekta);

            SaveChanges(context);


            return Ok(dioProjekta);
        }
    }
    }

