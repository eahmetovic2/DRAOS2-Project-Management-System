using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.Models.Response;
using Web.Models.Response.Common;
using Web.Models.Response.Projekat.ZahtjevKategorija;

namespace Emis.Web.Models.Response
{
    public class ZahtjevListModel : PagedListModel<ZahtjevListModelItem>
    {
		
    }

    public class ZahtjevListModelItem : BazniResponseModel
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public string ZahtjevKategorija { get; set; }

        public string ZahtjevStatus { get; set; }

        public string ZahtjevTip { get; set; }

        public string ZahtjevPrioritet { get; set; }

        public DateTime? DatumKreiranja { get; set; }
        public DateTime? PocetakIzrade { get; set; }

        public string CreatedBy { get; set; }

        public string Projekat { get; set; }

        public string DioProjekta { get; set; }

        /*[Required]
		public int ProjekatId {get; set;}
		public string DodijeljeniKorisnikIme {get; set;}
		/*[Required]
		public DateTime EstimiranoVrijeme {get; set;}
		[Required]
		public DateTime PotrošenoVrijeme {get; set;}
		[Required]
		public int ZahtjevKategorijaId {get; set;}
		[Required]
		public int ZahtjevPrioritetId {get; set;}
		[Required]
		public int ZahtjevStatusId {get; set;}
		[Required]
		public int ZahtjevTipId {get; set;}*/
    }
}
