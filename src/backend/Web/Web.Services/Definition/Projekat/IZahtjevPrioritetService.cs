using System;
using System.Collections.Generic;
using System.Text;
using Web.Entities.Models.Projekat;
using Web.Models.Request.Projekat.ZahtjevPrioritet;
using Web.Models.Response.Projekat.ZahtjevPrioritet;
using Web.Services.Result;

namespace Web.Services.Definition.Projekat
{
    public interface IZahtjevPrioritetService
    {
        ServiceResult<List<ZahtjevPrioritetModel>> VratiSveZahtjevePrioritetaProjekta(int projekatId);

        ServiceResult<List<ZahtjevPrioritet>> DodajNoviPrioritetZahtjevaProjekta(int projekatId, KreirajZahtjevPrioritetRequestModel zahtjevpPrioritetModel);
        ServiceResult<Nothing> AzurirajDefaultniZahtjevPrioritetProjekta(int projekatId, AzurirajDefaultniZahtjevPrioritetProjektaRequestModel zahtjevpPrioritetModel);


        //ServiceResult<ZahtjevModel> KreirajZahtjevZaProjekat(int projekatId, KreirajZahtjevRequestModel model);
    }
}
