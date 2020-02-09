using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models.Response.Korisnik.PravoAkcija
{
    public class PravoAkcijaListModel
    {
        public ICollection<PravoAkcijaListModelItem> Items { get; set; }
    }
}
