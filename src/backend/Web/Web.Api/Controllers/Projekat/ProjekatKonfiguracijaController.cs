using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Api.Auth.Requirements;
using Web.Api.Common.Attributes;
using Web.Models.Request.Base.Projekat;
using Web.Services;
using Web.Services.Definition.Projekat;

namespace Web.Api.Controllers.Projekat
{

    [Route("projekatkonfiguracija")]
    public class ProjekatKonfiguracijaController : BaseController
    {

        /// <summary>
        /// Servis za upravljanje korisnicima
        /// </summary>
        private IProjekatKonfiguracijaService projekatKonfiguracijaService;
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
        public ProjekatKonfiguracijaController(IProjekatKonfiguracijaService projekatKonfiguracijaService, ILogService logService, IAuthService authService)
        {
            this.projekatKonfiguracijaService = projekatKonfiguracijaService;
            this.logService = logService;
            this.authService = authService;
        }

        [HttpGet("projekat/{projekatId}")]

        public IActionResult VratiKonfiguracijuZaProjekat(int projekatId)
        {
            var result = projekatKonfiguracijaService.VratiKonfiguracijuZaProjekat(projekatId);
            return Convert(result);
        }


        [HttpPut("projekat/{projekatId:int}")]
        //[Authorize("UserIsAdmin")]
        [ClaimRequirement(ClaimTypes.UserData, "projekat_projekat_edit")]

        [RequireModel]
        public IActionResult AzurirajProjekat(int projekatId, [FromBody] AzurirajProjekatKonfiguracijaRequestModel model)
        {
            var result = projekatKonfiguracijaService.AzurirajProjekatKonfiguraciju(projekatId, model);
            return Convert(result);
        }
    }
}
