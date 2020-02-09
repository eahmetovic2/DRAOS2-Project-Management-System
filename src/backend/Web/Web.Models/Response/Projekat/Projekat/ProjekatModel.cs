using System;
using System.Collections.Generic;
using System.Text;
using Web.Models.Response.Common;

namespace Web.Models.Response.Base.Projekat
{

    public class ProjekatModel
    {
        public int Id { get; set; }
        public String Naziv { get; set; }
        public String Opis { get; set; }
        public DateTime? DatumIzmjene { get; set; }
    }
}



