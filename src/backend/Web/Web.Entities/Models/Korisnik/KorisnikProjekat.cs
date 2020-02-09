using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Web.Entities.Models.Korisnik
{
    public class KorisnikProjekat
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

       /* public string KorisnickoIme { get; set; }
        [JsonIgnore]
        [ForeignKey("KorisnickoIme")]
        public virtual Korisnik Korisnik { get; set; }*/

        public int KorisnikUlogaId { get; set; }
        [JsonIgnore]
        [ForeignKey("KorisnikUlogaId")]
        public virtual KorisnikUloga KorisnikUloga { get; set; }

        public int ProjekatId { get; set; }
        [JsonIgnore]
        [ForeignKey("ProjekatId")]
        public virtual Projekat.Projekat Projekat { get; set; }

        [JsonIgnore]
        public virtual ICollection<KorisnikKategorija> KorisnikKategorije { get; set; }
    }
}
