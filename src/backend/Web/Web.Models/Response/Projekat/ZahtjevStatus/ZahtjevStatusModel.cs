using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models.Response.Zahtjev.ZahtjevStatus
{
    public class ZahtjevStatusModel
    {
        public int Id { get; set; }

        public string Naziv { get; set; }

        public bool Default { get; set; }

        public int Oznaka { get; set; }
    }
}
