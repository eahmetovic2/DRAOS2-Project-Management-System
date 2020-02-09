using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models.Response.Projekat.ZahtjevTip
{
    public class ZahtjevTipModel
    {
        public int Id { get; set; }
        public string Naziv { get; set; }

        public bool Default { get; set; }
    }
}
