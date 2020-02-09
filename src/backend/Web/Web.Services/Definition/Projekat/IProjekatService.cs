using System;
using System.Collections.Generic;
using System.Text;
using Web.Entities.Models.Base;
using Web.Entities.Models.Projekat;
using Web.Models.Request.Base.Projekat;
using Web.Models.Request.Korisnik;
using Web.Models.Request.Projekat.Projekat;
using Web.Models.Response.Base.Projekat;
using Web.Models.Response.Korisnik.Korisnik;
using Web.Models.Response.Korisnik.KorisnikProjekti;
using Web.Services.Result;

namespace Web.Services.Definition.Base
{
    public interface IProjekatService
    {
        //ServiceResult<List<ProjekatModel>> VratiSveProjekte();
        ServiceResult<ProjekatListModel> VratiSveProjekte(ListaProjekataRequestModel model);


        ServiceResult<ProjekatModel> Kreiraj(KreirajProjekatRequestModel model);

        ServiceResult<ProjekatModel> VratiProjekatPoNazivu(string naziv);

        ServiceResult<ProjekatModel> AzurirajNazivProjekta(string naziv, AzurirajNazivProjektaRequestModel model);
        ServiceResult<Nothing> ObrisiProjekat(int id);
        ServiceResult<ProjekatModel> AzurirajProjekat(int projekatId, AzurirajProjekatRequestModel model);
        ServiceResult<ProjekatModel> VratiProjekat(int projekatId);

        ServiceResult<KorisnikListModel> VratiSveKorisnikeProjekta(int projekatId, ListaKorisnikaRequestModel model);

        ServiceResult<List<KorisnikProjektiModel>> VratiSveProjekteZaKorisnikUlogu(String korisnickoIme, int ulogaId);


    }
}
