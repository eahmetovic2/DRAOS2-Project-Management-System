using System;
using System.Collections.Generic;
using System.Text;
using Web.Models.Request.Zahtjev.ZahtjevKomentar;
using Web.Models.Response.Zahtjev.ZahtjevKomentar;
using Web.Services.Result;

namespace Web.Services.Definition.Zahtjev
{
    public interface IZahtjevKomentarService
    {
        ServiceResult<ZahtjevKomentariListModel> VratiSveKomentareZahtjeva(int projekatId, ListaKomentaraZahtjevaRequestModel model);

        ServiceResult<Nothing> KreirajKomentarZaZahtjev(int projekatId, KreirajKomentarRequestModel model);

    }
}
