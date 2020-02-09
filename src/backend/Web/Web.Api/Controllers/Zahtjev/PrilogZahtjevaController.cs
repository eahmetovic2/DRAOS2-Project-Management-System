using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Auth.Requirements;
using Web.Services;
using Web.Services.Definition.Base;

namespace Web.Api.Controllers.Zahtjev
{
    [Route("prilogZahtjeva")]

    public class PrilogZahtjevaController:BaseController
    {

        private IPrilogZahtjevaService prilogZahtjevaService;

        private ILogService logService;

        private IAuthService authService;

        public PrilogZahtjevaController(IPrilogZahtjevaService prilogZahtjevaService, ILogService logService, IAuthService authService)
        {
            this.prilogZahtjevaService = prilogZahtjevaService;
            this.logService = logService;
            this.authService = authService;
        }

        [HttpGet("{id}")]
        public IActionResult Download(int id)
        {
            var result = prilogZahtjevaService.Download(id);

            /* if (!result.IsOk)
                 return Convert(result);*/
            return Ok(result);

            //return File(result.Value.Bytes, result.Value.ContentType, result.Value.Naziv);
        }
    }
}
