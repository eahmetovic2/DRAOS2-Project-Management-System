using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Common.Attributes;
using Web.Models.Request.Zahtjev.ZahtjevKomentar;
using Web.Services;
using Web.Services.Definition.Zahtjev;

namespace Web.Api.Controllers.Zahtjev
{
    [Route("zahtjevkomentari")]

    public class ZahtjevKomentarController:BaseController
    {



        /// <summary>
        /// Servis za upravljanje korisnicima
        /// </summary>
        private IZahtjevKomentarService zahtjevKomentarService;
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
        public ZahtjevKomentarController(IZahtjevKomentarService zahtjevKomentarService, ILogService logService, IAuthService authService)
        {
            this.zahtjevKomentarService = zahtjevKomentarService;
            this.logService = logService;
            this.authService = authService;
        }

        [HttpGet("zahtjev/{zahtjevId}")]
        public IActionResult VratiSveKomentareZahtjeva(int zahtjevId, ListaKomentaraZahtjevaRequestModel model)
        {
            var result = zahtjevKomentarService.VratiSveKomentareZahtjeva(zahtjevId, model);
            return Convert(result);
        }

        [HttpPost("zahtjev/{zahtjevId}")]
        [RequireModel]
        public IActionResult KreirajKomentarZaZahtjev(int zahtjevId, [FromBody] KreirajKomentarRequestModel model)
        {
            var result = zahtjevKomentarService.KreirajKomentarZaZahtjev(zahtjevId, model);
            
            return Convert(result);
        }
    }
}
