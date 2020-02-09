using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Entities.Models.Base;
using Web.Entities.Models.Projekat;

namespace Web.Entities.Models.Zahtjev
{
    public class Zahtjev:EntityAutoriziran
    {

        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Naziv { get; set; }

        public string Opis { get; set; }
        //u sekundama izrazeno u bazi
        public long? EstimiranoVrijeme { get; set; }
        //u sekundama izrazeno u bazi
        public long? PotrošenoVrijeme { get; set; }
        public bool IsDeleted { get; set; }

        public int ProjekatId { get; set; }
        [JsonIgnore]
        [ForeignKey("ProjekatId")]
        public virtual Projekat.Projekat Projekat { get; set; }

        public int ZahtjevPrioritetId { get; set; }
        [JsonIgnore]
        [ForeignKey("ZahtjevPrioritetId")]
        public virtual ZahtjevPrioritet ZahtjevPrioritet { get; set; }

        public int ZahtjevStatusId { get; set; }
        [JsonIgnore]
        [ForeignKey("ZahtjevStatusId")]
        public ZahtjevStatus ZahtjevStatus { get; set; }

        public int ZahtjevKategorijaId { get; set; }
        [JsonIgnore]
        [ForeignKey("ZahtjevKategorijaId")]
        public ZahtjevKategorija ZahtjevKategorija { get; set; }

        public int ZahtjevTipId { get; set; }
        [JsonIgnore]
        [ForeignKey("ZahtjevTipId")]
        public ZahtjevTip ZahtjevTip { get; set; }

        public string DodijeljeniKorisnikIme { get; set; }
        [JsonIgnore]
        [ForeignKey("DodijeljeniKorisnikIme")]
        public Korisnik.Korisnik DodijeljeniKorisnik { get; set; }

        [JsonIgnore]
        public virtual ICollection<PrilogZahtjeva> PriloziZahtjeva { get; set; }

        [JsonIgnore]
        public virtual ICollection<IzmjenaZahtjeva> IzmjeneZahtjeva { get; set; }

        [JsonIgnore]
        public virtual ICollection<ZahtjevKomentar> Komentari { get; set; }
    }
}
