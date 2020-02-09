using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Web.Entities.Models.Korisnik
{
    public class KorisnikKategorija
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string KorisnickoIme { get; set; }
        [JsonIgnore]
        [ForeignKey("KorisnickoIme")]
        public virtual Korisnik Korisnik { get; set; }

        public int ZahtjevKategorijaId { get; set; }
        [JsonIgnore]
        [ForeignKey("ZahtjevKategorijaId")]
        public virtual Projekat.ZahtjevKategorija ZahtjevKategorija { get; set; }
    }
}
