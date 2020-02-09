using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Web.Entities.Models.Projekat
{
    public class ZahtjevKategorija
    {

        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public string Naziv { get; set; }

        public int DioProjektaId { get; set; }
        [JsonIgnore]
        [ForeignKey("DioProjektaId")]
        public virtual DioProjekta DioProjekta { get; set; }

        [JsonIgnore]
        public virtual ICollection<Zahtjev.Zahtjev> Zahtjevi { get; set; }

        [JsonIgnore]
        public virtual ICollection<Korisnik.KorisnikKategorija> KorisnikKategorije { get; set; }

    }
}
