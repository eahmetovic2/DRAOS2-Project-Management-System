using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Api.Auth.Requirements;
using Web.Api.Common.Attributes;
using Web.Models.Request.Projekat.DioProjekta;
using Web.Services;
using Web.Services.Definition.Projekat;

namespace Web.Api.Controllers.Projekat
{
    [Route("dioprojekta")]

    public class DioProjektaController:BaseController
    {

        /// <summary>
        /// Servis za upravljanje korisnicima
        /// </summary>
        private IDioProjektaService dioProjektaService;
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
        public DioProjektaController(IDioProjektaService dioProjektaService, ILogService logService, IAuthService authService)
        {
            this.dioProjektaService = dioProjektaService;
            this.logService = logService;
            this.authService = authService;
        }

        [HttpGet("projekat/{projekatId}")]
        public IActionResult VratiSveDijeloveProjekta(int projekatId)
        {
            var result = dioProjektaService.VratiSveDijeloveProjekta(projekatId);
            return Convert(result);
        }

        [HttpPost("projekat/{projekatId}")]
        [RequireModel]

        public IActionResult DodajNoviDioProjekta(int projekatId, [FromBody] KreirajDioProjektaRequestModel dioProjektaModel)
        {
            var result = dioProjektaService.DodajNoviDioProjekta(projekatId, dioProjektaModel);
            return Convert(result);
        }

    }
}
