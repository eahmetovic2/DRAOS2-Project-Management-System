using System;
using System.Collections.Generic;
using System.Text;
using Web.Models.Request.Korisnik.Uloga;
using Web.Models.Response.Korisnik.PravoAkcijaUloga;
using Web.Models.Response.Korisnik.Uloga;
using Web.Services.Result;

namespace Web.Services.Definition.Korisnik
{
    public interface IUlogaService
    {
        ServiceResult<UlogaListModel> VratiSveZaKorisnickoIme(string korisnickoIme);

        ServiceResult<Nothing> SnimiDozvoljeneAkcije(int ulogaId, SnimiDozvoljeneAkcijeRequestModel model);

        ServiceResult<PravoAkcijaUlogaListModel> VratiSveDozvoljeneAkcije(int ulogaId);

        ServiceResult<GrupaPravaUlogeListModel> VratiSveGrupePravaProsireno(int ulogaId);

        ServiceResult<UlogaModel> Kreiraj(KreirajUloguRequestModel model);

        ServiceResult<UlogaModel> Azuriraj(int ulogaId, AzurirajUloguRequestModel model);

        ServiceResult<UlogaListModel> VratiSve();

        ServiceResult<UlogaModel> VratiPoIdu(int ulogaId);
    }
}
