using Web.Api.Common.Attributes;
using Web.Models.Request.Postavke;
using Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Controllers.Base
{
    /// <summary>
    /// Kontroler za upravljanje postavkama
    /// </summary>
    [Route("postavke/")]
    public class PostavkeController : BaseController
    {
        /// <summary>
        /// Servis za upravljanje osobama
        /// </summary>
        private IPostavkeService postavkeService;

        /// <summary>
        /// Konstruktor kontrolera
        /// </summary>
        public PostavkeController(IPostavkeService postavkeService)
        {
            this.postavkeService = postavkeService;
        }

        [HttpGet("")]
        [AllowAnonymous]
        public IActionResult Vrati()
        {
            var result = postavkeService.VratiPostavke();
            return Convert(result);
        }

        [HttpPut("")]        
        [RequireModel]
        public IActionResult Azuriraj([FromBody] AzurirajPostavkeRequestModel model)
        {
            var result = postavkeService.AzurirajPostavke(model);
            return Convert(result);
        }
    }
}
