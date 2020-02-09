using System;
using System.Collections.Generic;
using System.Text;
using Web.Models.Request.Korisnik.KorisnikDodatneInformacije;
using Web.Models.Response.Korisnik.UlogaTipDodatneInformacije;
using Web.Services.Result;

namespace Web.Services.Definition.Korisnik
{
    public interface IUlogaTipoviDodatneInformacijeService
    {
        ServiceResult<Nothing> SnimiZaUlogu(int ulogaId, SnimiDodatneInformacijeUlogeRequestModel model);

        ServiceResult<UlogaTipDodatneInformacijeListModel> VratiZaUlogu(int ulogaId);
    }
}
