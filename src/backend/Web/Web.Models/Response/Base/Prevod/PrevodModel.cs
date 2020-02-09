using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models.Response.Base.Prevod
{
    public class PrevodModel
    {
        public int Jezik { get; set; }
        public String JezikNaziv { get; set; }
        public List<PoljePrevodModel> Polja { get; set; }
    }

    public class PoljePrevodModel
    {
        public String Polje { get; set; }
        public String Vrijednost { get; set; }
        public String Prevod { get; set; }

    }
}
