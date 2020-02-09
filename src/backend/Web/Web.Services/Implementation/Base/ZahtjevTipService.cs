using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities;
using Web.Entities.Models.Projekat;
using Web.Models.Mapping.Mappers.Base.ProjekatMap;
using Web.Models.Request.Projekat.ZahtjevTip;
using Web.Models.Response.Projekat.ZahtjevPrioritet;
using Web.Models.Response.Projekat.ZahtjevTip;
using Web.Services.Definition.Projekat;
using Web.Services.Result;

namespace Web.Services.Implementation.Base
{

    public class ZahtjevTipService : Service, IZahtjevTipService
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
        public ZahtjevTipService(ILifetimeScope scope, Context context, IAuthService authService, IApplicationConfigurationService applicationConfigurationService)
            : base(scope)
        {
            this.context = context;
            this.authService = authService;
            this.applicationConfigurationService = applicationConfigurationService;

        }

        public ServiceResult<List<ZahtjevTipModel>> VratiSveTipoveZahtjevaProjekta(int projekatId)
        {

            var zahtjevPrioriteti = context.ZahtjevTipovi
                .Where(p => p.ProjekatId == projekatId).ToZahtjevTipModel().ToList();
            if (zahtjevPrioriteti == null)
                return NotFound();

            return Ok(zahtjevPrioriteti);
        }



        public ServiceResult<ZahtjevTip> DodajNoviTipZahtjevaProjekta(int projekatId, KreirajZahtjevTipRequestModel zahtjevTipModel)
        {

            if (zahtjevTipModel.Naziv.Length > 20)
                return Error("Naziv ne može biti veći od 20 karaktera");

            ZahtjevTip zahtjevTip = new ZahtjevTip();
            zahtjevTip.Default = false;
            zahtjevTip.Naziv = zahtjevTipModel.Naziv;
            zahtjevTip.ProjekatId = projekatId;

            context.Add(zahtjevTip);

            SaveChanges(context);


            return Ok(zahtjevTip);
        }

        public ServiceResult<Nothing> AzurirajDefaultniZahtjevTipProjekta(int projekatId, AzurirajDefaultniZahtjevTipProjektaRequestModel zahtjevTipModel)
        {
            var zahtjevTipovi = context.ZahtjevTipovi
                          .Where(p => p.ProjekatId == projekatId).ToList();

            foreach (var z in zahtjevTipovi)
            {
                if (z.Id == zahtjevTipModel.Id)
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
