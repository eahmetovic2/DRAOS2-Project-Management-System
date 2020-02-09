using System;
using System.Collections.Generic;
using System.Text;
using Web.Entities.Models.Zahtjev;
using Web.Models.Response.Common;

namespace Web.Models.Response.Zahtjev.ZahtjevKomentar
{
    public class ZahtjevKomentariListModel: PagedListModel<ZahtjevKomentarListModelItem>
    {
    }
    public class ZahtjevKomentarListModelItem : BazniResponseModel
    {
        public int Id { get; set; }
        public string Komentar { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DatumKreiranja { get; set; }
        public List<Dokument> PriloziKomentara { get; set; }

    }
}

