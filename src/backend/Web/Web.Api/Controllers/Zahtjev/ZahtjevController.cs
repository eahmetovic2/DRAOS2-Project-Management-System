using Emis.Web.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Api.Auth.Requirements;
using Web.Api.Common.Attributes;
using Web.Models.Request.Zahtjev.Zahtjev;
using Web.Services;
using Web.Services.Definition.Base;

namespace Web.Api.Controllers.Base
{


    [Route("zahtjevi")]
    public class ZahtjevController : BaseController
    {

        /// <summary>
        /// Servis za upravljanje korisnicima
        /// </summary>
        private IZahtjevService zahtjevService;
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
        public ZahtjevController(IZahtjevService zahtjevService, ILogService logService, IAuthService authService)
        {
            this.zahtjevService = zahtjevService;
            this.logService = logService;
            this.authService = authService;
        }

        [HttpGet("")]
        public IActionResult VratiSveZahtjeve([FromQuery]ListaZahtjevaRequestModel model)
        {
            var result = zahtjevService.VratiSveZahtjeve(model);
            return Convert(result);
        }

        [HttpGet("projekat/{projekatId}")]
        public IActionResult VratiSveZahtjeveProjekta(int projekatId,[FromQuery]ListaZahtjevaProjektaRequestModel model)
        {
            var result = zahtjevService.VratiSveZahtjeveProjekta(projekatId, model);
            return Convert(result);
        }

        [HttpGet("{zahtjevId}")]
        public IActionResult VratiZahtjev(int zahtjevId)
        {
            var result = zahtjevService.VratiZahtjev(zahtjevId);
            return Convert(result);
        }
        /*
        [HttpGet("{naziv}")]
        //[Authorize("UserIsAdminOrOwner")]
        public IActionResult VratiJedan(String naziv)
        {
            try
            {
                var result = zahtjevService.VratiProjekatPoNazivu(naziv);
                return Convert(result);
            }
            catch (Exception e)
            {
                throw;
            }
        }*/


        [HttpPost("projekat/{projekatId}")]
        [RequireModel]
        public IActionResult KreirajZahtjevZaProjekat(int projekatId,[FromBody] KreirajZahtjevRequestModel model)
        {
            var result = zahtjevService.KreirajZahtjevZaProjekat(projekatId,model);
            if (result.IsOk)
            {
                /*logService.Akcija(Core.Constants.LogLevel.Info,
                         Core.Constants.LogKategorija.korisnik,
                         Core.Constants.LogAkcija.korisnik_dodaj,
                         "KorisnickoIme: " + result.Value.KorisnickoIme,
                        authService.TrenutniKorisnik().KorisnickoIme
                         );*/

            }
            return Convert(result);
        }


        [HttpPut("{zahtjevId}")]
        [RequireModel]

        public IActionResult AzurirajZahtjev(int zahtjevId, [FromBody] AzurirajZahtjevRequestModel model)
        {
            var result = zahtjevService.AzurirajZahtjev(zahtjevId, model);
            if (result.IsOk)
            {
                logService.Akcija(Core.Constants.LogLevel.Info,
                        Core.Constants.LogKategorija.zahtjevi,
                        Core.Constants.LogAkcija.zahtjev_izmijeni,
                        "Zahtjev: " + (result.Value.Id),//+result.Value.Naziv+result.Value.Opis+result.Value.ZahtjevKategorija+result.Value.ZahtjevPrioritet+result.Value.ZahtjevTip,
                        authService.TrenutniKorisnik().KorisnickoIme
                        );
            }
            return Convert(result);
        }

        [HttpPut("{zahtjevId:int}/zahtjevStatus")]
        [RequireModel]
        //[ClaimRequirement(ClaimTypes.UserData, "zahtjev_zahtjev_edit_status")]

        public IActionResult AzurirajStatusZahtjeva(int zahtjevId, [FromBody]AzurirajStatusZahtjevaRequestModel model)
        {
            var result = zahtjevService.AzurirajStatusZahtjeva(zahtjevId, model);
            return Convert(result);
        }


        [HttpGet("{zahtjevId:int}/zahtjevStatusi")]
        //[ClaimRequirement(ClaimTypes.UserData, "zahtjev_zahtjev_edit_status")]

        public IActionResult VratiIzmjeneStatusaZahtjeva(int zahtjevId)
        {
            var result = zahtjevService.VratiIzmjeneStatusaZahtjeva(zahtjevId);
            return Convert(result);
        }
        [HttpPut("{zahtjevId:int}/brisanje")]
        //[ClaimRequirement(ClaimTypes.UserData, "zahtjev_zahtjev_edit_status")]
        public IActionResult ObrisiZahtjev(int zahtjevId)
        {
            var result = zahtjevService.ObrisiZahtjev(zahtjevId);
            return Convert(result);
        }
        /*
        [HttpPut("{stariNaziv}")]
        //[Authorize("UserIsAdmin")]
        [RequireModel]
        public IActionResult AzurirajNazivProjekta(String stariNaziv, [FromBody] AzurirajNazivProjektaRequestModel model)
        {
            var result = projekatService.AzurirajNazivProjekta(stariNaziv, model);
            return Convert(result);
        }

        [HttpDelete("{projekatId}")]
        //[Authorize("UserIsAdminOrOwner")]
        public IActionResult Obrisi(int projekatId)
        {
            var result = projekatService.ObrisiProjekat(projekatId);
            return Convert(result);
        }*/




    }
}
    

