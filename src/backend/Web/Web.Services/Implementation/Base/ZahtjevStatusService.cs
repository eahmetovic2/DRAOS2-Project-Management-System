using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities;
using Web.Entities.Models.Projekat;
using Web.Models.Mapping.Mappers.Base.ProjekatMap;
using Web.Models.Request.Projekat.ZahtjevStatus;
using Web.Models.Response.Projekat.ZahtjevPrioritet;
using Web.Models.Response.Projekat.ZahtjevTip;
using Web.Models.Response.Zahtjev.ZahtjevStatus;
using Web.Services.Definition.Projekat;
using Web.Services.Result;

namespace Web.Services.Implementation.Base
{

    public class ZahtjevStatusService : Service, IZahtjevStatusService
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
        public ZahtjevStatusService(ILifetimeScope scope, Context context, IAuthService authService, IApplicationConfigurationService applicationConfigurationService)
            : base(scope)
        {
            this.context = context;
            this.authService = authService;
            this.applicationConfigurationService = applicationConfigurationService;

        }

        public ServiceResult<List<ZahtjevStatusModel>> VratiSveStatuseZahtjevaProjekta(int projekatId)
        {

            var zahtjevStatusi = context.ZahtjevStatusi
                .Where(p => p.ProjekatId == projekatId).OrderBy(p => p.Oznaka).ToZahtjevStatusModel().ToList();
            if (zahtjevStatusi == null)
                return NotFound();

            return Ok(zahtjevStatusi);
        }

        public ServiceResult<List<ZahtjevStatus>> DodajNoviStatusZahtjevaProjekta(int projekatId, KreirajZahtjevStatusRequestModel zahtjevStatusModel)
        {
            if (zahtjevStatusModel.Naziv.Length > 20)
                return Error("Naziv ne može biti veći od 20 karaktera");
            var brojZahtjevStatusaProjekta = context.ZahtjevStatusi
             .Where(p => p.ProjekatId == projekatId).Count();

            if (brojZahtjevStatusaProjekta >= 6)
                return Error("Ukupan broj statusa zahtjeva projekta ne može biti veći od 6.");



            ZahtjevStatus zahtjevStatus = new ZahtjevStatus();
            zahtjevStatus.Default = false;
            zahtjevStatus.Naziv = zahtjevStatusModel.Naziv;
            zahtjevStatus.Oznaka = zahtjevStatusModel.Oznaka;
            zahtjevStatus.ProjekatId = projekatId;

            context.Add(zahtjevStatus);
           
            SaveChanges(context);

            var zahtjevStatusi = context.ZahtjevStatusi
              .Where(p => p.ProjekatId == projekatId).OrderBy(p => p.Oznaka).ToList();

            return Ok(zahtjevStatusi);
        }

        public ServiceResult<Nothing> AzurirajDefaultniZahtjevStatusProjekta(int projekatId, AzurirajDefaultniZahtjevStatusProjektaRequestModel zahtjevStatusModel)
        {
            var zahtjevStatusi = context.ZahtjevStatusi
                          .Where(p => p.ProjekatId == projekatId).ToList();

            foreach (var z in zahtjevStatusi)
            {
                if (z.Id == zahtjevStatusModel.Id)
                {
                    z.Default = true;
                }
                else z.Default = false;
            }

            SaveChanges(context);

            return Ok();
        }

        public ServiceResult<Nothing> AzurirajPoredakStatusa(int projekatId, AzurirajPoredakZahtjevStatusaRequestModel poredakZahtjevStatusaModel)
        {
            for(int i=0;i<poredakZahtjevStatusaModel.ZahtjeviId.Count;i++)
            {
                var zahtjev = context.ZahtjevStatusi.Where(z => z.Id == poredakZahtjevStatusaModel.ZahtjeviId[i]).First();
                zahtjev.Poredak = i;
            }
            context.SaveChanges();

            return Ok();
        }
    }


}
