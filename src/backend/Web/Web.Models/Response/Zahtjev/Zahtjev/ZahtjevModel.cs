using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.Entities.Models.Projekat;
using Web.Entities.Models.Zahtjev;
using Web.Models.Response.Zahtjev;

namespace Emis.Web.Models.Response
{
    public class ZahtjevModel
    {
        public int Id { get; set; }
		public string Naziv {get; set;}
		public string Opis {get; set;}

        public string ZahtjevKategorija { get; set; }

        public string ZahtjevTip { get; set; }

        public string ZahtjevPrioritet { get; set; }
        public string DioProjekta { get; set; }

        public string CreatedBy { get; set; }

        public int ProjekatId { get; set; }

        public string Projekat { get; set; }

        public string PotrosenoVrijeme { get; set; }

        public string DodijeljeniKorisnikIme { get; set; }

        public DateTime? DatumKreiranja { get; set; }
        public DateTime? DatumIzmjene { get; set; }

        public ZahtjevStatusModel ZahtjevStatus { get; set; }

        public List<Dokument> Dokumenti { get; set; }

    }
}
