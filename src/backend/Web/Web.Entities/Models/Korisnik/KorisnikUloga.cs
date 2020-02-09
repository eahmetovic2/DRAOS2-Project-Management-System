using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Web.Entities.Models.Sifarnik;

namespace Web.Entities.Models.Korisnik
{
    public class KorisnikUloga
    {

        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KorisnikUlogaId { get; set; }

        [StringLength(64)]
        public string KorisnickoIme { get; set; }

        [JsonIgnore]
        [ForeignKey("KorisnickoIme")]
        public virtual Korisnik Korisnik { get; set; }


        public int UlogaId { get; set; }

        [JsonIgnore]
        [ForeignKey("UlogaId")]
        public virtual Uloga Uloga { get; set; }

        [JsonIgnore]
        public virtual ICollection<KorisnikUlogaDodatnaInformacija> KorisnikUlogaDodatnaInformacija { get; set; }

        [JsonIgnore]
        public virtual ICollection<KorisnikProjekat> KorisnikProjekti { get; set; }
    }
}
