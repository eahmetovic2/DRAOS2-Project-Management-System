using Web.Entities.Models;
using Web.Entities.Models.Base;
using Web.Models.Response.Postavke;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models.Mapping.Mappers.PostavkeMap
{
    public static class PostavkeMapper
    {
        public static PostavkeModel ToPostavkeModel(this Postavke postavke)
        {
            return new PostavkeModel()
            {
                NaslovSistema = postavke.NaslovSistema,
                TrajanjeSesije = postavke.TrajanjeSesije,
                UrlKarte = postavke.UrlKarte,
                AutorskaPravaKarte = postavke.AutorskaPravaKarte
            };
        }
    }
}