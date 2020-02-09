using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Api.Auth.Requirements;
using Web.Api.Common.Attributes;
using Web.Models.Request.Projekat.ZahtjevStatus;
using Web.Services;
using Web.Services.Definition.Base;
using Web.Services.Definition.Projekat;

namespace Web.Api.Controllers.Projekat
{
    [Route("zahtjevstatus")]

    public class ZahtjevStatusController : BaseController
    {

        /// <summary>
        /// Servis za upravljanje korisnicima
        /// </summary>
        private IZahtjevStatusService zahtjevStatusService;
        /// <summary>
        /// Servis za logiranje
        /// </summary> 
        private ILogService logService;
        /// <summary>
        /// Servis za autentikaciju
        /// </summary>
        private IAuthService authService;
        /// <summary>
        /// Konstruktor kontrolera
        /// </summary>
        public ZahtjevStatusController(IZahtjevStatusService zahtjevStatusService, ILogService logService, IAuthService authService)
        {
            this.zahtjevStatusService = zahtjevStatusService;
            this.logService = logService;
            this.authService = authService;
        }

        [HttpGet("projekat/{projekatId}")]
        public IActionResult VratiSveStatuseZahtjevaProjekta(int projekatId)
        {
            var result = zahtjevStatusService.VratiSveStatuseZahtjevaProjekta(projekatId);
            return Convert(result);
        }


        [HttpPost("projekat/{projekatId}")]
        [RequireModel]

        public IActionResult DodajNoviStatusZahtjevaProjekta(int projekatId, [FromBody] KreirajZahtjevStatusRequestModel zahtjevStatusModel)
        {
            var result = zahtjevStatusService.DodajNoviStatusZahtjevaProjekta(projekatId, zahtjevStatusModel);
            return Convert(result);
        }


        [HttpPut("projekat/{projekatId}")]
        [RequireModel]
        [ClaimRequirement(ClaimTypes.UserData, "projekat_projekat_edit")]

        public IActionResult NoviDefaultniPrioritetZahtjevaProjekta(int projekatId, [FromBody] AzurirajDefaultniZahtjevStatusProjektaRequestModel zahtjevStatusModel)
        {
            var result = zahtjevStatusService.AzurirajDefaultniZahtjevStatusProjekta(projekatId, zahtjevStatusModel);
            return Convert(result);
        }

        [HttpPut("projekat/{projekatId}/poredak")]
        [RequireModel]
        [ClaimRequirement(ClaimTypes.UserData, "projekat_projekat_edit")]

        public IActionResult AzurirajPoredakStatusa(int projekatId, [FromBody] AzurirajPoredakZahtjevStatusaRequestModel zahtjevStatusModel)
        {
            var result = zahtjevStatusService.AzurirajPoredakStatusa(projekatId, zahtjevStatusModel);
            return Convert(result);
        }

    }
}
