using Autofac;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities;
using Web.Models.Mapping.Mappers.Base.ProjekatMap;
using Web.Models.Request.Base.Projekat;
using Web.Models.Response.Projekat.ProjekatKonfiguracija;
using Web.Services.Definition.Projekat;
using Web.Services.Result;

namespace Web.Services.Implementation.Base
{

        public class ProjekatKonfiguracijaService : Service, IProjekatKonfiguracijaService
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
            public ProjekatKonfiguracijaService(ILifetimeScope scope, Context context, IAuthService authService, IApplicationConfigurationService applicationConfigurationService)
                : base(scope)
            {
                this.context = context;
                this.authService = authService;
                this.applicationConfigurationService = applicationConfigurationService;

            }

        public ServiceResult<ProjekatKonfiguracijaModel> VratiKonfiguracijuZaProjekat(int projekatId)
        {

            var projekat = context.Projekti
                .FirstOrDefault(p => p.Id == projekatId);

            var projekatKonfiguracija = context.ProjekatKonfiguracija.
                ToProjekatKonfiguracijaModel().
                SingleOrDefault(p => p.Id == projekat.ProjekatKonfiguracijaId);

            if (projekatKonfiguracija == null)
                return NotFound();

            return Ok(projekatKonfiguracija);
        }


        public ServiceResult<ProjekatKonfiguracijaModel> AzurirajProjekatKonfiguraciju(int projekatId,AzurirajProjekatKonfiguracijaRequestModel model)
        {
            if (model.RadnoVrijemeOd >= model.RadnoVrijemeDo)
                return Error("Početak radnog vremena mora biti prije kraja radnog vremena.");

            var projekatKonfiguracija = context.Projekti
                                .Include(x => x.ProjekatKonfiguracija)
                                  .SingleOrDefault(p => p.Id == projekatId).ProjekatKonfiguracija;
            if (projekatKonfiguracija == null)
                return NotFound();


            projekatKonfiguracija.RadnoVrijemeOd = model.RadnoVrijemeOd;
            projekatKonfiguracija.RadnoVrijemeDo = model.RadnoVrijemeDo;
            projekatKonfiguracija.RadniDani =model.RadniDani;

            SaveChanges(context);

            return Ok(projekatKonfiguracija.ToProjekatKonfiguracijaModel());
        }


    }


   

}
