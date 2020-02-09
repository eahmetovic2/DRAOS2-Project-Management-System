using System;
using System.Collections.Generic;
using System.Text;
using Web.Models.Response.Korisnik.PravoAkcija;

namespace Web.Models.Response.Korisnik.PravoObjekt
{
    public class PravoObjektListModelItem
    {
        public int Id { get; set; }

        public string Naziv { get; set; }

        public PravoAkcijaListModel PravoAkcije { get; set; }
    }
}
