using Web.Models.Response.Sifarnik;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models.Response.Sifarnik
{
    public class SifarnikList : List<SifarnikModel>
    {
        public DateTime DatumResponsa { get; set; }
    }
}
