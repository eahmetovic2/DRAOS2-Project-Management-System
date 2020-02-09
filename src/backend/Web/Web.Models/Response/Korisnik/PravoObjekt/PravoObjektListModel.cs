using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models.Response.Korisnik.PravoObjekt
{
    public class PravoObjektListModel
    {
        public ICollection<PravoObjektListModelItem> Items { get; set; }
    }
}
