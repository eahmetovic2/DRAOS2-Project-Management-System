using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Common.Attributes;
using Web.Models.Request.Base.Notifikacija;
using Web.Services;
using Web.Services.Definition.Korisnik;

namespace Web.Api.Controllers.Korisnik
{
    [Route("notifikacije")]

    public class NotifikacijaController : BaseController
    {
		private INotifikacijaService notifikacijaService;
        
        private ILogService logService;
        
        private IAuthService authService;
        
        public NotifikacijaController(INotifikacijaService notifikacijaService, ILogService logService, IAuthService authService)
        {
            this.notifikacijaService = notifikacijaService;
            this.logService = logService;
            this.authService = authService;
        }


        [HttpGet("")]
        public IActionResult VratiNotifikacije()
        {
            var result = notifikacijaService.VratiNotifikacijeZaKorisnika();
            return Convert(result);
        }


        [HttpPut("otvori")]
        [RequireModel]

        public IActionResult OtvoriNotifikaciju([FromBody]OtvoriNotifikacijuRequestModel model)
        {
            var result = notifikacijaService.OtvoriNotifikaciju(model);
            return Convert(result);
        }

        [HttpPut("zahtjev/{zahtjevId}/otvori")]
        public IActionResult OtvoriNotifikaciju(int zahtjevId)
        {
            var result = notifikacijaService.OtvoriKorisnikoveNotifikacijeZahtjeva(zahtjevId);
            return Convert(result);
        }
    }
}
