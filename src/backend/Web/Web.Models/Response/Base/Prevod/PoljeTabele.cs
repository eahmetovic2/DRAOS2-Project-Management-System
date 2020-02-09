using System;
using System.Collections.Generic;
using System.Text;
using Web.Core.Constants;

namespace Web.Models.Response.Base.Prevod
{
    public class PoljeTabele
    {
        public String Naziv { get; set; }
        public TipPolja Tip { get; set; }


        public PoljeTabele(string naziv, TipPolja tip)
        {

            Naziv = naziv;
            Tip = tip;
        }
    }
}
