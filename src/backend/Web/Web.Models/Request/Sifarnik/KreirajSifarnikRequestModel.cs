using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models.Request.Sifarnik
{
    public class KreirajSifarnikRequestModel
    {
        public string TipSifarnika { get; set; }
        public Dictionary<string, object> Sifarnik { get; set; }
    }
}