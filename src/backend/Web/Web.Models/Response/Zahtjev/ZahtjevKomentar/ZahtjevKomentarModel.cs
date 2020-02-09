using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models.Response.Zahtjev.ZahtjevKomentar
{
    public class ZahtjevKomentarModel
    {
        public int Id { get; set; }
        public string Komentar { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DatumKreiranja { get; set; }
    }
}
