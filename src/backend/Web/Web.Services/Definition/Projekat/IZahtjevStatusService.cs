using System;
using System.Collections.Generic;
using System.Text;
using Web.Entities.Models.Projekat;
using Web.Models.Request.Projekat.ZahtjevStatus;
using Web.Models.Response.Projekat.ZahtjevPrioritet;
using Web.Models.Response.Projekat.ZahtjevTip;
using Web.Models.Response.Zahtjev.ZahtjevStatus;
using Web.Services.Result;

namespace Web.Services.Definition.Projekat
{
    public interface IZahtjevStatusService
    {
        ServiceResult<List<ZahtjevStatusModel>> VratiSveStatuseZahtjevaProjekta(int projekatId);
        ServiceResult<List<ZahtjevStatus>> DodajNoviStatusZahtjevaProjekta(int projekatId, KreirajZahtjevStatusRequestModel zahtjevStatusModel);
        ServiceResult<Nothing> AzurirajDefaultniZahtjevStatusProjekta(int projekatId, AzurirajDefaultniZahtjevStatusProjektaRequestModel zahtjevpPrioritetModel);
        ServiceResult<Nothing> AzurirajPoredakStatusa(int projekatId, AzurirajPoredakZahtjevStatusaRequestModel poredakZahtjevStatusaModel);


        //ServiceResult<ZahtjevModel> KreirajZahtjevZaProjekat(int projekatId, KreirajZahtjevRequestModel model);
    }
}
