using System;
using System.Collections.Generic;
using System.Text;
using Web.Models.Request.Base.Projekat;
using Web.Models.Response.Projekat.ProjekatKonfiguracija;
using Web.Services.Result;

namespace Web.Services.Definition.Projekat
{
    public interface IProjekatKonfiguracijaService
    {
        ServiceResult<ProjekatKonfiguracijaModel> VratiKonfiguracijuZaProjekat(int projekatId);

        ServiceResult<ProjekatKonfiguracijaModel> AzurirajProjekatKonfiguraciju(int projekatId, AzurirajProjekatKonfiguracijaRequestModel model);
    }
}