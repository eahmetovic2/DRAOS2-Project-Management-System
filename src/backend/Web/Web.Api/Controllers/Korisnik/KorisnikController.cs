using Web.Api.Common.Attributes;
using Web.Models.Request.Korisnik;
using Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Web.Models.Request.Korisnik.Korisnik;
using Web.Api.Auth.Requirements;
using System.Security.Claims;

namespace Web.Api.Controllers.Korisnik
{
    /// <summary>
    /// Kontroler za upravljanje korisnicima
    /// </summary>
    [Route("korisnici")]
	public class KorisnikController : BaseController
	{
		/// <summary>
		/// Servis za upravljanje korisnicima
		/// </summary>
		private IKorisnikService korisnikService;
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
		public KorisnikController(IKorisnikService korisnikService, ILogService logService, IAuthService authService)
		{
			this.korisnikService = korisnikService;
			this.logService = logService;
			this.authService = authService;
		}

		[HttpGet("")]
        [ClaimRequirement(ClaimTypes.UserData, "korisnik_korisnik_lista")]
        public IActionResult VratiSve([FromQuery]ListaKorisnikaRequestModel model)
		{
			var result = korisnikService.VratiSve(model);
			return Convert(result);
		}

		[HttpGet("{korisnickoIme}")]
       // [ClaimRequirement(ClaimTypes.UserData, "korisnik_korisnik_pregled")]
        //[Authorize("UserIsAdminOrOwner")]
        public IActionResult VratiJedan(String korisnickoIme)
		{
			try
			{
				var result = korisnikService.VratiKorisnikaPoKorisnickomImenu(korisnickoIme);
				return Convert(result);
			}
			catch (Exception e)
			{
				throw;
			}
        }

        [HttpGet("jezik")]
        public IActionResult DajJezik()
        {
            var result = korisnikService.DajJezik();
            return Convert(result);
        }

        [HttpPost("jezik")]
        [RequireModel]
        [ClaimRequirement(ClaimTypes.UserData, "korisnik_korisnik_izmjena_licnih_podataka")]
        public IActionResult AzirirajJezik([FromBody] AzurirajJezikKorisnikaRequestModel model)
        {
            var result = korisnikService.AzurirajJezik(model);
            return Convert(result);
        }

        [HttpPut("{korisnickoIme}")]
        [ClaimRequirement(ClaimTypes.UserData, "korisnik_korisnik_izmjena")]
        //[Authorize("UserIsAdminOrOwner")]
        [RequireModel]
		public IActionResult Azuriraj(String korisnickoIme, [FromBody] AzurirajKorisnikaRequestModel model)
		{
			var result = korisnikService.AzurirajKorisnika(korisnickoIme, model);
			if (result.IsOk)
			{
				logService.Akcija(Core.Constants.LogLevel.Info,
						Core.Constants.LogKategorija.korisnik,
						Core.Constants.LogAkcija.korisnik_izmijeni,
						"KorisnickoIme: " + result.Value.KorisnickoIme,
						authService.TrenutniKorisnik().KorisnickoIme
						);
			}
			return Convert(result);
		}

        [HttpPut("{korisnickoIme}/licni-detalji")]
        //[ClaimRequirement(ClaimTypes.UserData, "korisnik_korisnik_izmjena_licnih_podataka")]
        [RequireModel]
        public IActionResult AzurirajLicneDetalje(String korisnickoIme, [FromBody] AzurirajLicneDetaljeRequestModel model)
        {
            var result = korisnikService.AzurirajLicneDetalje(korisnickoIme, model);
            if (result.IsOk)
            {
                logService.Akcija(Core.Constants.LogLevel.Info,
                        Core.Constants.LogKategorija.korisnik,
                        Core.Constants.LogAkcija.korisnik_izmijeni,
                        "KorisnickoIme: " + result.Value.KorisnickoIme,
                        authService.TrenutniKorisnik().KorisnickoIme
                        );
            }
            return Convert(result);
        }

        [HttpPut("{korisnickoIme}/lozinka")]
		[RequireModel]
        [ClaimRequirement(ClaimTypes.UserData, "korisnik_korisnik_promjena_lozinke")]
        public IActionResult PromijeniLozinku(String korisnickoIme, [FromBody] PromijeniLozinkuRequestModel model)
		{
			var result = korisnikService.PromijeniLozinku(korisnickoIme, model.Lozinka, model.NovaLozinka);
            if (result.IsOk)
			{
				logService.Akcija(Core.Constants.LogLevel.Info,
						 Core.Constants.LogKategorija.korisnik,
						 Core.Constants.LogAkcija.korisnik_dodaj,
						 "KorisnickoIme: " + result.Value,
						authService.TrenutniKorisnik().KorisnickoIme
						 );

			}
			return Convert(result);
		}

		[HttpPost("{korisnickoIme}/lozinka")]
		[RequireModel]
        [ClaimRequirement(ClaimTypes.UserData, "korisnik_korisnik_promjena_lozinke")]
        public IActionResult PostaviLozinku(String korisnickoIme, [FromBody] PostaviLozinkuRequestModel model)
		{
			var result = korisnikService.PostaviLozinku(korisnickoIme, model.NovaLozinka);
			return Convert(result);
		}

		[HttpPut("{korisnickoIme}/onemogucen")]
		[RequireModel]
        [ClaimRequirement(ClaimTypes.UserData, "korisnik_korisnik_aktivacija")]
        public IActionResult PostaviKorisnikOnemogucen(String korisnickoIme, [FromBody] PostaviKorisnikOnemogucenRequestModel model)
		{
			var result = korisnikService.PostaviKorisnikOnemogucen(korisnickoIme, model.Onemogucen);
			return Convert(result);
		}

		[HttpPost("")]
		[RequireModel]
        [ClaimRequirement(ClaimTypes.UserData, "korisnik_korisnik_dodavanje")]
        public IActionResult Kreiraj([FromBody] KreirajKorisnikaRequestModel model)
		{
			var result = korisnikService.Kreiraj(model);
			if (result.IsOk)
			{
				logService.Akcija(Core.Constants.LogLevel.Info,
						 Core.Constants.LogKategorija.korisnik,
						 Core.Constants.LogAkcija.korisnik_dodaj,
						 "KorisnickoIme: " + result.Value.KorisnickoIme,
						authService.TrenutniKorisnik().KorisnickoIme
						 );

			}
			return Convert(result);
		}


        [HttpGet("zahtjevKategorija/{zahtjevKategorijaId}")]
        [ClaimRequirement(ClaimTypes.UserData, "zahtjev_zahtjev_edit_dodijeljeni_korisnik")]
        public IActionResult VratiJedan(int zahtjevKategorijaId)
        {
            try
            {
                var result = korisnikService.VratiSveSupportKorisnikeZaZahtjevKategoriju(zahtjevKategorijaId);
                return Convert(result);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /*[HttpPost("{korisnickoIme}/projekti")]
        [RequireModel]
        public IActionResult DodajKorisnikaNaProjekte(String korisnickoIme,[FromBody] AzurirajProjekteKorisnikaRequestModel model)
        {
            var result = korisnikService.DodajKorisnikaNaProjekat(korisnickoIme,model);
            return Convert(result);
        }*/



    }
}
