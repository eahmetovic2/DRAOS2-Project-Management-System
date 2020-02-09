using System;
using System.Collections.Generic;
using System.Text;
using Web.Models.Response.Base.Prevod;
using Web.Services.Result;

namespace Web.Services.Definition.Base
{
    public interface IPrevodService : IService
    {
        ServiceResult<List<PrevodModel>> VratiListuPrevoda(String tabela, int id);
        ServiceResult<List<PrevodModel>> KreirajPrevod(String tabela, int id, List<PrevodModel> model);
        String DajPrevod(String tabela, int? jezik, int id, String polje, String tekst);
        ServiceResult<List<PrevodModel>> GetEntitetPrevod<TEntity>(String tabela, int id) where TEntity : class;
    }
}
