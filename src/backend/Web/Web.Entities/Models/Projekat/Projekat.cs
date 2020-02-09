
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Entities.Models.Base;

namespace Web.Entities.Models.Projekat
{
    public class Projekat: EntityAutoriziran
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

     
        public string Naziv { get; set; }

        
        public string Opis { get; set; }

        public int ProjekatKonfiguracijaId { get; set; }
        [JsonIgnore]
        [ForeignKey("ProjekatKonfiguracijaId")]
        public virtual ProjekatKonfiguracija ProjekatKonfiguracija { get; set; }

        [JsonIgnore]
        public virtual ICollection<Zahtjev.Zahtjev> Zahtjevi { get; set; }

        [JsonIgnore]
        public virtual ICollection<ZahtjevStatus> StatusiZahtjeva { get; set; }

        [JsonIgnore]
        public virtual ICollection<ZahtjevTip> TipoviZahtjeva { get; set; }

        [JsonIgnore]
        public virtual ICollection<ZahtjevPrioritet> PrioritetiZahtjeva { get; set; }

        [JsonIgnore]
        public virtual ICollection<DioProjekta> DijeloviProjekta { get; set; }

        [JsonIgnore]
        public virtual ICollection<Korisnik.KorisnikProjekat> Korisnici { get; set; }

        [JsonIgnore]
        public virtual ICollection<Notifikacija> Notifikacije { get; set; }



    }
}
