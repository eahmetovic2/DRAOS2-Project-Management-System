using System;
using System.Collections.Generic;
using System.Text;
using Web.Entities.Models.Projekat;
using Web.Models.Request.Projekat.DioProjekta;
using Web.Models.Response.Projekat.DioProjekta;
using Web.Services.Result;

namespace Web.Services.Definition.Projekat
{
    public interface IDioProjektaService
    {
        ServiceResult<List<DioProjektaModel>> VratiSveDijeloveProjekta(int projekatId);
        ServiceResult<DioProjekta> DodajNoviDioProjekta(int projekatId, KreirajDioProjektaRequestModel dioProjektaModel);
    }
}
