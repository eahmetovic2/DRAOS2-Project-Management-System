using Web.Services;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Definition.Base;
using System;
using Web.Models.Response.Base.Prevod;
using System.Collections.Generic;

namespace Web.Api.Controllers.Base
{
    [Route("prevod/")]
    public class PrevodController : BaseController
    {
        private IPrevodService prevodService;
        private IAuthService authService;
        public PrevodController(IPrevodService prevodService, IAuthService authService)
        {
            this.prevodService = prevodService;
            this.authService = authService;
        }

        [HttpPost("{tabela}/id/{id}")]

        public IActionResult KreirajPrevod(String tabela, int id, [FromBody] List<PrevodModel> model)
        {
            var prevod = prevodService.KreirajPrevod(tabela, id, model);
            return Ok(prevod);
        }
        [HttpGet("{tabela}/id/{id}")]

        public IActionResult GetListuPrevoda(String tabela, int id)
        {
            var prevod = prevodService.VratiListuPrevoda(tabela, id);
            return Ok(prevod);
        }
    }
}
