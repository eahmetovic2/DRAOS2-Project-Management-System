using System;
using System.Collections.Generic;
using System.Text;
using Web.Entities.Models.Projekat;
using Web.Models.Request.Projekat.ZahtjevKategorija;
using Web.Models.Response.Projekat.ZahtjevKategorija;
using Web.Services.Result;

namespace Web.Services.Definition.Projekat
{
    public interface IZahtjevKategorijaService
    {

        ServiceResult<List<ZahtjevKategorijaModel>> VratiSveKategorijeZahtjevaDijelaProjekta(int dioProjektaId);
        ServiceResult<ZahtjevKategorija> DodajNovuKategorijuZahtjevaDijelaProjekta(int dioProjektaId, KreirajZahtjevKategorijaRequestModel zahtjevKategorijaModel);
        ServiceResult<List<ZahtjevKategorijaModel>> VratiSveKategorijeKorisnika(string korisnickoIme);
    }
}
