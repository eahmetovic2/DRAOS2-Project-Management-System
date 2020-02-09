using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Web.Models.Request.Zahtjev.ZahtjevKomentar
{
    public class KreirajKomentarRequestModel
    {
        public string Komentar { get; set; }

        public int KorisnikId { get; set; }

        public int? DokumentId { get; set; }

    }
}
