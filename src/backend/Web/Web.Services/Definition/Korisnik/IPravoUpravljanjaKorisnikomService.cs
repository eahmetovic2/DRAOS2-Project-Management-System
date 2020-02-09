using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities.Models.Korisnik;
using Web.Models.Response.Korisnik.PravoUpravljanjaKorisnikom;
using Web.Services.Result;

namespace Web.Services.Definition.Korisnik
{
    public interface IPravoUpravljanjaKorisnikomService : IService
    {
        ServiceResult<PravoUpravljanjaKorisnikomListModel> VratiSve(int ulogaId);

        IQueryable<PravoUpravljanjaKorisnikom> VratiPravaUpravljanjaKorisnikom(int ulogaId);
    }
}
