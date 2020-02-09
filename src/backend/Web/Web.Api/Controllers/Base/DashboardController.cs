using Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers.Base
{
    [Route("dashboard")]
    public class DashboardController : BaseController
    {
        private IDashboardService dashboardService;

        public DashboardController(IDashboardService dashboardService, ITokenService tokenService)
        {
            this.dashboardService = dashboardService;
        }

        [HttpGet("osnovno")]
        [ResponseCache(Duration = 10080, Location = ResponseCacheLocation.Any, VaryByHeader = "X-AUTH-TOKEN")]
        public IActionResult OsnovnoDashboard()
        {
            var result = dashboardService.OsnovnoDashboard();

            return Convert(result);
        }

        [HttpGet("header")]
        [ResponseCache(Duration = 10080, Location = ResponseCacheLocation.Any, VaryByHeader = "X-AUTH-TOKEN")]
        public IActionResult Header()
        {
            var result = dashboardService.Header();

            return Convert(result);
        }

        [HttpGet("statistika")]
        //[ResponseCache(Duration = 10080, Location = ResponseCacheLocation.Any, VaryByHeader = "X-AUTH-TOKEN")]
        public IActionResult Statistika()
        {
            var result = dashboardService.VratiStatistikuZaDashboard();

            return Convert(result);
        }

        [HttpGet("zavrseniZahtjeviPoDanimaProslaSedmica")]
        public IActionResult ZavršeniZahtjeviPoDanimaProšlaSedmica()
        {
            var result = dashboardService.ZavršeniZahtjeviPoDanimaProšlaSedmica();

            return Convert(result);
        }

        [HttpGet("kreiraniZahtjeviPoDanimaProslaSedmica")]
        public IActionResult KreiraniZahtjeviPoDanimaProšlaSedmica()
        {
            var result = dashboardService.KreiraniZahtjeviPoDanimaProšlaSedmica();

            return Convert(result);
        }

        [HttpGet("brojZahtjevaPoProjektu")]
        public IActionResult BrojZahtjevaPoProjektu()
        {
            var result = dashboardService.BrojZahtjevaPoProjektu();

            return Convert(result);
        }

        [HttpGet("zahtjeviNajduzeUPotrebnoUraditi")]
        public IActionResult ZahtjeviNajduzeUPotrebnoUraditi()
        {
            var result = dashboardService.ZahtjeviNajduzeUPotrebnoUraditi();

            return Convert(result);
        }

        [HttpGet("zahtjeviZadnjiDodatiKojeJePotrebnoUraditi")]
        public IActionResult ZahtjeviZadnjiDodatiKojeJePotrebnoUraditi()
        {
            var result = dashboardService.ZahtjeviZadnjiDodatiKojeJePotrebnoUraditi();

            return Convert(result);
        }
        [HttpGet("zahtjeviPoDanimaAktivnaGodina")]
        public IActionResult ZahtjeviPoDanimaAktivnaGodina()
        {
            var result = dashboardService.ZahtjeviPoDanimaAktivnaGodina();

            return Convert(result);
        }
        [HttpGet("zahtjeviZadnjiDodatiNisuRijeseni")]
        public IActionResult ZahtjeviZadnjiDodatiNisuRijeseni()
        {
            var result = dashboardService.ZahtjeviZadnjiDodatiNisuRijeseni();

            return Convert(result);
        }
        [HttpGet("zahtjeviZadnjiDodatiRijeseni")]
        public IActionResult ZahtjeviZadnjiDodatiRijeseni()
        {
            var result = dashboardService.ZahtjeviZadnjiDodatiRijeseni();

            return Convert(result);
        }

        [HttpGet("zahtjeviPoMjesecima")]
        public IActionResult ZahtjeviPoMjesecima()
        {
            var result = dashboardService.ZahtjeviPoMjesecima();

            return Convert(result);
        }


        [HttpGet("zahtjeviPoSedmicama")]
        public IActionResult ZahtjeviPoSedmicama()
        {
            var result = dashboardService.ZahtjeviPoSedmicama();

            return Convert(result);
        }




        /*[HttpGet("sviProjektiKorisnika")]
        public IActionResult VratiSveProjekteKorisnika(ListaProjekataRequestModel model)
        {
            var result = dashboardService.VratiSveProjekteKorisnika(model);

            return Convert(result);
        }*/







    }
}
