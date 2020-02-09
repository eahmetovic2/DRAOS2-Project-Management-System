using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Api.Auth.Requirements;
using Web.Api.Common.Attributes;
using Web.Models.Request.Projekat.ZahtjevPrioritet;
using Web.Services;
using Web.Services.Definition.Base;
using Web.Services.Definition.Projekat;

namespace Web.Api.Controllers.Projekat
{
    [Route("zahtjevprioritet")]

    public class ZahtjevPrioritetController:BaseController
    {

        /// <summary>
        /// Servis za upravljanje korisnicima
        /// </summary>
        private IZahtjevPrioritetService zahtjevPrioritetService;
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
        public ZahtjevPrioritetController(IZahtjevPrioritetService zahtjevPrioritetService, ILogService logService, IAuthService authService)
        {
            this.zahtjevPrioritetService = zahtjevPrioritetService;
            this.logService = logService;
            this.authService = authService;
        }

        [HttpGet("projekat/{projekatId}")]
        public IActionResult VratiSveZahtjevePrioritetaProjekta(int projekatId)
        {
            var result = zahtjevPrioritetService.VratiSveZahtjevePrioritetaProjekta(projekatId);
            return Convert(result);
        }

        [HttpPost("projekat/{projekatId}")]
        [RequireModel]

        public IActionResult DodajNoviPrioritetZahtjevaProjekta(int projekatId, [FromBody] KreirajZahtjevPrioritetRequestModel zahtjevPrioritetModel)
        {
            var result = zahtjevPrioritetService.DodajNoviPrioritetZahtjevaProjekta(projekatId, zahtjevPrioritetModel);
            return Convert(result);
        }
        [HttpPut("projekat/{projekatId}")]
        [RequireModel]
        [ClaimRequirement(ClaimTypes.UserData, "projekat_projekat_edit")]

        public IActionResult NoviDefaultniPrioritetZahtjevaProjekta(int projekatId, [FromBody] AzurirajDefaultniZahtjevPrioritetProjektaRequestModel zahtjevPrioritetModel)
        {
            var result = zahtjevPrioritetService.AzurirajDefaultniZahtjevPrioritetProjekta(projekatId, zahtjevPrioritetModel);
            return Convert(result);
        }




    }
}
