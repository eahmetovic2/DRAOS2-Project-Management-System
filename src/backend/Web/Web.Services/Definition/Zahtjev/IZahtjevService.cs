using Emis.Web.Models.Request;
using Emis.Web.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Entities.Models.Base;
using Web.Entities.Models.Zahtjev;
using Web.Models.Request.Zahtjev.Zahtjev;
using Web.Services.Result;

namespace Web.Services.Definition.Base
{
    
        public interface IZahtjevService
        {
        ServiceResult<ZahtjevListModel> VratiSveZahtjeve(ListaZahtjevaRequestModel model);

        ServiceResult<ZahtjevListModel> VratiSveZahtjeveProjekta(int projekatId, ListaZahtjevaProjektaRequestModel model);

        ServiceResult<ZahtjevModel> KreirajZahtjevZaProjekat(int projekatId, KreirajZahtjevRequestModel model);

        ServiceResult<ZahtjevModel> VratiZahtjev(int zahtjevId);
        ServiceResult<ZahtjevModel> AzurirajZahtjev(int zahtjeviId, AzurirajZahtjevRequestModel model);
        ServiceResult<Nothing> AzurirajStatusZahtjeva(int zahtjevId, AzurirajStatusZahtjevaRequestModel model);

        ServiceResult<List<IzmjenaZahtjeva>> VratiIzmjeneStatusaZahtjeva(int zahtjevId);

        ServiceResult<Nothing> ObrisiZahtjev(int zahtjevId);



    }
}
