using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Api.Auth.Requirements;
using Web.Api.Common.Attributes;
using Web.Models.Request.Projekat.ZahtjevTip;
using Web.Services;
using Web.Services.Definition.Base;
using Web.Services.Definition.Projekat;

namespace Web.Api.Controllers.Projekat
{
    [Route("zahtjevtip")]

    public class ZahtjevTipController : BaseController
    {

        /// <summary>
        /// Servis za upravljanje korisnicima
        /// </summary>
        private IZahtjevTipService zahtjevTipService;
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
        public ZahtjevTipController(IZahtjevTipService zahtjevTipService, ILogService logService, IAuthService authService)
        {
            this.zahtjevTipService = zahtjevTipService;
            this.logService = logService;
            this.authService = authService;
        }

        [HttpGet("projekat/{projekatId}")]
        public IActionResult VratiSveTipoveZahtjevaProjekta(int projekatId)
        {
            var result = zahtjevTipService.VratiSveTipoveZahtjevaProjekta(projekatId);
            return Convert(result);
        }

        [HttpPost("projekat/{projekatId}")]
        [RequireModel]

        public IActionResult DodajNoviTipZahtjevaProjekta(int projekatId, [FromBody] KreirajZahtjevTipRequestModel zahtjevTipModel)
        {
            var result = zahtjevTipService.DodajNoviTipZahtjevaProjekta(projekatId, zahtjevTipModel);
            return Convert(result);
        }

        [HttpPut("projekat/{projekatId}")]
        [RequireModel]
        [ClaimRequirement(ClaimTypes.UserData, "projekat_projekat_edit")]

        public IActionResult NoviDefaultniTipZahtjevaProjekta(int projekatId, [FromBody] AzurirajDefaultniZahtjevTipProjektaRequestModel zahtjevTipModel)
        {
            var result = zahtjevTipService.AzurirajDefaultniZahtjevTipProjekta(projekatId, zahtjevTipModel);
            return Convert(result);
        }

    }
}
