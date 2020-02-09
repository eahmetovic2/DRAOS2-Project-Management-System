using System;
using System.Collections.Generic;
using System.Text;
using Web.Entities.Models.Projekat;
using Web.Models.Request.Projekat.ZahtjevTip;
using Web.Models.Response.Projekat.ZahtjevPrioritet;
using Web.Models.Response.Projekat.ZahtjevTip;
using Web.Services.Result;

namespace Web.Services.Definition.Projekat
{
    public interface IZahtjevTipService
    {
        ServiceResult<List<ZahtjevTipModel>> VratiSveTipoveZahtjevaProjekta(int projekatId);
        ServiceResult<ZahtjevTip> DodajNoviTipZahtjevaProjekta(int projekatId, KreirajZahtjevTipRequestModel zahtjevTipModel);
        ServiceResult<Nothing> AzurirajDefaultniZahtjevTipProjekta(int projekatId, AzurirajDefaultniZahtjevTipProjektaRequestModel zahtjevTipModel);


        //ServiceResult<ZahtjevModel> KreirajZahtjevZaProjekat(int projekatId, KreirajZahtjevRequestModel model);
    }
}
