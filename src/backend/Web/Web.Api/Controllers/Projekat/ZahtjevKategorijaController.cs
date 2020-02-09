using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Common.Attributes;
using Web.Models.Request.Projekat.ZahtjevKategorija;
using Web.Services;
using Web.Services.Definition.Projekat;

namespace Web.Api.Controllers.Projekat
{
    [Route("zahtjevkategorija")]

    public class ZahtjevKategorijaController:BaseController
    {


        /// <summary>
        /// Servis za upravljanje korisnicima
        /// </summary>
        private IZahtjevKategorijaService zahtjevKategorijaService;
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
        public ZahtjevKategorijaController(IZahtjevKategorijaService zahtjevKategorijaService, ILogService logService, IAuthService authService)
        {
            this.zahtjevKategorijaService = zahtjevKategorijaService;
            this.logService = logService;
            this.authService = authService;
        }

        [HttpGet("dioprojekta/{dioProjektaId}")]
        public IActionResult VratiSveKategorijeZahtjevaDijelaProjekta(int dioProjektaId)
        {
            var result = zahtjevKategorijaService.VratiSveKategorijeZahtjevaDijelaProjekta(dioProjektaId);
            return Convert(result);
        }
        [HttpGet("{korisnickoIme}")]
        public IActionResult VratiSveKategorijeKorisnika(string korisnickoIme)
        {
            var result = zahtjevKategorijaService.VratiSveKategorijeKorisnika(korisnickoIme);
            return Convert(result);
        }

        [HttpPost("dioprojekta/{dioProjektaId}")]
        [RequireModel]

        public IActionResult DodajNovuKategorijuZahtjevaDijelaProjekta(int dioProjektaId, [FromBody] KreirajZahtjevKategorijaRequestModel dioProjektaModel)
        {
            var result = zahtjevKategorijaService.DodajNovuKategorijuZahtjevaDijelaProjekta(dioProjektaId, dioProjektaModel);
            return Convert(result);
        }
    }
}
