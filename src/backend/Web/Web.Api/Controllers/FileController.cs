using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emis.Web.Api.Controllers
{
    /// <summary>
    /// Kontroler za upravljanje uploadom datoteka
    /// </summary>
    [Route("file/")]
    public class UploadController : EmisController
    {
        INastavniPlanService nastavniPlanService;

        public NastavniPlanController(INastavniPlanService nastavniPlanService)
        {
            this.nastavniPlanService = nastavniPlanService;
        }
    }
}
