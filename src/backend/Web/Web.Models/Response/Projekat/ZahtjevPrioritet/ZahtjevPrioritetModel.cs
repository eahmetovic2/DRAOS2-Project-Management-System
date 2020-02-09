using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models.Response.Projekat.ZahtjevPrioritet
{
    public class ZahtjevPrioritetModel
    {
        public int Id { get; set; }
        public string Naziv { get; set; }

        public int Poredak { get; set; }

        public bool Default { get; set; }

        //public int ProjekatId { get; set; }
    }
}
