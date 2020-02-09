using Web.Entities.Models;
using Web.Entities.Models.Base;
using Web.Models.Request.Postavke;
using Web.Models.Response.Postavke;
using Web.Services.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Services
{
    /// <summary>
    /// Servis koji radi sa postavkama sistema
    /// </summary>
    public interface IPostavkeService : IService
    {
        /// <summary>
        /// Vrati trenutne postavke sistema
        /// Ako postavke ne postoje, vrati default postavke
        /// </summary>
        /// <returns>Model postavki sistema</returns>
        ServiceResult<PostavkeModel> VratiPostavke();

        /// <summary>
        /// Azuriraj postavke sistema
        /// </summary>
        /// <param name="model">Nove postavke sistema</param>
        /// <returns>Model azuriranih postavki</returns>
        ServiceResult<PostavkeModel> AzurirajPostavke(AzurirajPostavkeRequestModel model);

        Postavke DajSistemskePostavke();
    }
}
