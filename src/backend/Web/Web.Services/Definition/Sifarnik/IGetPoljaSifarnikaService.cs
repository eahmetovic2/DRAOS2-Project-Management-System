using System;
using System.Collections.Generic;
using System.Text;
using Web.Core.Constants;
using Web.Models.Response.Sifarnik;
using static Web.Services.Implementation.SifarnikService;

namespace Web.Services.Definition.Sifarnik
{
    public interface IGetPoljaSifarnikaService : IService
    {
        Dictionary<ESifarnik, Func<InputParameters, SifarnikListModel>> GetPoljaSifarnika();
    }
}
