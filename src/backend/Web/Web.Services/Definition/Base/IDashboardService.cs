using Emis.Web.Models.Response;
using System.Collections.Generic;
using Web.Models.Response.Base.Dashboard;
using Web.Models.Response.Dashboard;
using Web.Services.Result;

namespace Web.Services
{
    public interface IDashboardService : IService
    {
        ServiceResult<DashboardModel> OsnovnoDashboard();

        ServiceResult<HeaderModel> Header();
        ServiceResult<DashboardModel> VratiStatistikuZaDashboard();
        ServiceResult<List<ZahtjevListModelItem>> ZadnjiZahtjeviKojiSuRijeseni();
        ServiceResult<List<BrojZahtjevaPoProjektuModel>> BrojZahtjevaPoProjektu();
        ServiceResult<List<int>> ZavršeniZahtjeviPoDanimaProšlaSedmica();
        ServiceResult<List<int>> KreiraniZahtjeviPoDanimaProšlaSedmica();
        ServiceResult<List<ZahtjevListModelItem>> ZahtjeviNajduzeUPotrebnoUraditi();
        ServiceResult<List<ZahtjevListModelItem>> ZahtjeviZadnjiDodatiKojeJePotrebnoUraditi();
        ServiceResult<List<int>> ZahtjeviPoDanimaAktivnaGodina();
        ServiceResult<List<ZahtjevListModelItem>> ZahtjeviZadnjiDodatiNisuRijeseni();
        ServiceResult<List<ZahtjevListModelItem>> ZahtjeviZadnjiDodatiRijeseni();
        ServiceResult<List<int>> ZahtjeviPoMjesecima();
        ServiceResult<List<int>> ZahtjeviPoSedmicama();






    }
}
