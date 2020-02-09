using Web.Core.Constants;
using Web.Models.Request.Sifarnik;
using Web.Models.Response.Sifarnik;
using System;
using System.Collections.Generic;

namespace Web.Services
{
    /// <summary>
    /// Servis koji radi sa nastavnicima
    /// </summary>
    public interface ISifarnikService : IService
    {
        /// <summary>
        /// samoDatum se koristi za etag a DatumIzmjene za filtriranje
        /// </summary>
        /// <param name="sifarnik"></param>
        /// <param name="samoDatum"></param>
        /// <param name="DatumIzmjene"></param>
        /// <returns></returns>
        SifarnikList VratiSve(ESifarnik sifarnik, bool samoDatum, DateTime? datumIzmjene);

        SifarnikListModel VratiSveSaPoljima(ESifarnik sifarnik, ListaSifarnikRequestModel model);

        List<PoljeSifarnika> VratiPolja(ESifarnik sifarnik);

        bool SnimiSifarnik(ESifarnik sifarnik, KreirajSifarnikRequestModel model);

        SifarnikModel DajSifarnik(ESifarnik tipSifarnika, int id);

        bool UpdateSifarnik(ESifarnik tipSifarnika, UpdateSifarnikRequestModel model);
    }

    
}