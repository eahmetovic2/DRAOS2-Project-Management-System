using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Emis.Web.Models.Request
{
    public class KreirajZahtjevRequestModel
    {
        public string Naziv {get; set;}
		public string Opis {get; set;}
		[Required]
		public int ProjekatId {get; set;}
		public string DodijeljeniKorisnikIme {get; set; }
		public DateTime PocetakIzrade { get; set; }
		public DateTime EstimiranoVrijeme {get; set;}
		public DateTime PotrošenoVrijeme {get; set;}
		[Required]
		public int ZahtjevKategorijaId {get; set;}
		[Required]
		public int ZahtjevPrioritetId {get; set;}
		[Required]
		public int ZahtjevTipId {get; set;}
        public int? DokumentId { get; set; }
    }
}
