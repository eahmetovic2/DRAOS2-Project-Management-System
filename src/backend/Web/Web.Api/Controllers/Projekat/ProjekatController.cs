using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Api.Auth.Requirements;
using Web.Api.Common.Attributes;
using Web.Models.Request.Base.Projekat;
using Web.Models.Request.Korisnik;
using Web.Models.Request.Projekat.Projekat;
using Web.Services;
using Web.Services.Definition.Base;

namespace Web.Api.Controllers.Base
{
    [Route("projekat")]
    public class ProjekatController:BaseController
    {
        
            /// <summary>
            /// Servis za upravljanje korisnicima
            /// </summary>
            private IProjekatService projekatService;
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
            public ProjekatController(IProjekatService projekatService, ILogService logService, IAuthService authService)
            {
                this.projekatService = projekatService;
                this.logService = logService;
                this.authService = authService;
            }

            [HttpGet("")]
            public IActionResult VratiSve([FromQuery]ListaProjekataRequestModel model)
            {
                var result = projekatService.VratiSveProjekte(model);
                return Convert(result);
            }

            [HttpGet("nazivProjekta/{naziv}")]
            
            //[Authorize("UserIsAdminOrOwner")]
            public IActionResult VratiJedan(String naziv)
            {
                try
                {
                    var result = projekatService.VratiProjekatPoNazivu(naziv);
                    return Convert(result);
                }
                catch (Exception e)
                {
                    throw;
                }
            }



        [HttpGet("{projekatId:int}")]
        [ClaimRequirement(ClaimTypes.UserData, "projekat_projekat_pregled")]
        public IActionResult VratiProjekat(int projekatId)
        {
            try
            {
                var result = projekatService.VratiProjekat(projekatId);
                return Convert(result);
            }
            catch (Exception e)
            {
                throw;
            }
        }


        [HttpGet("korisnik/{korisnickoIme}/uloga/{ulogaId:int}")]
        //[Authorize("UserIsAdminOrOwner")]
        public IActionResult VratiProjekteZaKorisnikUlogu(String korisnickoIme,int ulogaId)
        {
            try
            {
                var result = projekatService.VratiSveProjekteZaKorisnikUlogu(korisnickoIme,ulogaId);
                return Convert(result);
            }
            catch (Exception e)
            {
                throw;
            }
        }




        /*

        [HttpPut("{korisnickoIme}/licni-detalji")]
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
        //[Authorize("UserIsAdminOrOwner")]
        [RequireModel]
        public IActionResult PromijeniLozinku(String korisnickoIme, [FromBody] PromijeniLozinkuRequestModel model)
        {
            var result = korisnikService.PromijeniLozinku(korisnickoIme, model.Lozinka, model.NovaLozinka);
            return Convert(result);
        }

        [HttpPost("{korisnickoIme}/lozinka")]
        //[Authorize("UserIsAdmin")]
        [RequireModel]
        public IActionResult PostaviLozinku(String korisnickoIme, [FromBody] PostaviLozinkuRequestModel model)
        {
            var result = korisnikService.PostaviLozinku(korisnickoIme, model.NovaLozinka);
            return Convert(result);
        }

        */

        [HttpPost("")]
            [RequireModel]
            public IActionResult Kreiraj([FromBody] KreirajProjekatRequestModel model)
            {
                var result = projekatService.Kreiraj(model);
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

        [HttpPut("{stariNaziv}")]
        //[Authorize("UserIsAdmin")]
        [RequireModel]
        public IActionResult AzurirajNazivProjekta(String stariNaziv, [FromBody] AzurirajNazivProjektaRequestModel model)
        {
            var result=projekatService.AzurirajNazivProjekta(stariNaziv, model);
            return Convert(result);
        }

        [HttpPut("{projekatId:int}")]
        //[Authorize("UserIsAdmin")]
        [RequireModel]
        [ClaimRequirement(ClaimTypes.UserData, "projekat_projekat_edit")]

        public IActionResult AzurirajProjekat(int projekatId, [FromBody] AzurirajProjekatRequestModel model)
        {
            var result = projekatService.AzurirajProjekat(projekatId, model);
            return Convert(result);
        }

        [HttpDelete("{projekatId}")]
        //[Authorize("UserIsAdminOrOwner")]
        public IActionResult Obrisi(int projekatId)
        {
            var result = projekatService.ObrisiProjekat(projekatId);
            return Convert(result);
        }

        [HttpGet("{projekatId:int}/korisnici")]
        [ClaimRequirement(ClaimTypes.UserData, "projekat_projekat_pregled")]

        //[Authorize("UserIsAdminOrOwner")]
        public IActionResult VratiSveKorisnikeProjekta(int projekatId,[FromQuery] ListaKorisnikaRequestModel model)
        {
            try
            {
                var result = projekatService.VratiSveKorisnikeProjekta(projekatId,model);
                return Convert(result);
            }
            catch (Exception e)
            {
                throw;
            }
        }





    }
    }
