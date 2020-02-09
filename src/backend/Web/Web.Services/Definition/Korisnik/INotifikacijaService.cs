using System;
using System.Collections.Generic;
using System.Text;
using Web.Entities.Models.Base;
using Web.Entities.Models.Korisnik;
using Web.Models.Request.Base.Notifikacija;
using Web.Services.Result;

namespace Web.Services.Definition.Korisnik
{
    public interface INotifikacijaService : IService
    {
        ServiceResult<List<Notifikacija>> VratiNotifikacijeZaKorisnika();

        ServiceResult<Nothing> OtvoriNotifikaciju(OtvoriNotifikacijuRequestModel model);
        ServiceResult<Nothing> OtvoriKorisnikoveNotifikacijeZahtjeva(int zahtjevId);



    }
}


