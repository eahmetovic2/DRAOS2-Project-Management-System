using Web.Api.Common.Attributes;
using Web.Api.Common.Extensions;
using Web.Models.Request.Token;
using Web.Services;
using Web.UserAgent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Request.Korisnik;
using Web.Entities;
using Web.Services.Definition.Korisnik;

namespace Web.Api.Controllers.Korisnik
{
    /// <summary>
    /// Kontroler za upravljanje tokenima korisnika
    /// </summary>
    [Route("korisnici/{korisnickoIme}/tokeni")]
    public class TokenController : BaseController
    {
        /// <summary>
        /// Servis za upravljanje korisnicima
        /// </summary>
        private IKorisnikService korisnikService;

        /// <summary>
        /// Servis za upravljanje tokenima
        /// </summary>
        private ITokenService tokenService;

        /// <summary>
        /// Parser za prepoznavanje klijenta
        /// </summary>
        private IUserAgentParser userAgentParser;


        private IUlogaService ulogaService;

        /// <summary>
        /// Servis za logiranje
        /// </summary>
        private ILogService logService;
              

        /// <summary>
        /// Konstruktor kontrolera
        /// </summary>
        public TokenController(IKorisnikService korisnikService, ITokenService tokenService, IUserAgentParser userAgentParser, ILogService logService, IUlogaService ulogaService)
        {
            this.korisnikService = korisnikService;
            this.tokenService = tokenService;
            this.userAgentParser = userAgentParser;
            this.logService = logService;
            this.ulogaService = ulogaService;

        }

        [HttpGet("")]
        //[Authorize("UserIsAdminOrOwner")]
        public IActionResult VratiSve(String korisnickoIme, [FromQuery]ListaTokenaRequestModel model)
        {
            var result = tokenService.VratiTokenePoKorisnickomImenu(korisnickoIme, model);
            return Convert(result);
        }

        [HttpGet("{tokenId:guid}")]
        [AllowAnonymous]
        public IActionResult VratiJedan(String korisnickoIme, Guid tokenId)
        {
            var result = tokenService.VratiTokenPoIdu(korisnickoIme, tokenId);
            return Convert(result);
        }

        [HttpPost("provjerilozinku")]
        [AllowAnonymous]
        [RequireModel]
        public IActionResult ProvjeriLozinku(String korisnickoIme, [FromBody] KreirajTokenRequestModel model)
        {
            var valid = korisnikService.ProvjeriLozinku(korisnickoIme, model.Lozinka);
            if (!valid.IsOk)
            {
                return Convert(valid);
            }
            var result = ulogaService.VratiSveZaKorisnickoIme(korisnickoIme);
            return Convert(result);
        }

        [HttpPost("")]
        [AllowAnonymous]
        [RequireModel]
        public IActionResult Kreiraj(String korisnickoIme, [FromBody] KreirajTokenRequestModel model)
        {
            var valid = korisnikService.ProvjeriLozinku(korisnickoIme, model.Lozinka);
            if (!valid.IsOk)
                return Convert(valid);

            var ip = Request.GetIpAddress();
            var klijent = Request.ParseUserAgent(userAgentParser);
            var result = tokenService.KreirajToken(korisnickoIme, model.UlogaId, ip, klijent, Core.Constants.TokenTip.Sesija);
           
                logService.Akcija(Core.Constants.LogLevel.Info,
                                    Core.Constants.LogKategorija.Pristup_sistemu,
                                    Core.Constants.LogAkcija.Prijava_na_sistem,
                                    ip,
                                    korisnickoIme);
            
            return Convert(result);
        }

        [HttpPost("temp")]
        [Authorize]
        public IActionResult KreirajTemp(String korisnickoIme, int ulogaId)
        {
            var ip = Request.GetIpAddress();
            var klijent = Request.ParseUserAgent(userAgentParser);
            var result = tokenService.KreirajToken(korisnickoIme, ulogaId, ip, klijent, Core.Constants.TokenTip.Temp);         
            return Convert(result);
        }

        [HttpDelete("{tokenId:guid}")]
        //[Authorize("UserIsAdminOrOwner")]
        public IActionResult Obrisi(String korisnickoIme, Guid tokenId)
        {
            var result = tokenService.ObrisiToken(korisnickoIme, tokenId);
            logService.InfoAkcija(Core.Constants.LogKategorija.Pristup_sistemu, Core.Constants.LogAkcija.Odjava_sa_sistema, string.Empty);
            return Convert(result);
        }
    }
}
