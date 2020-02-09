using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Web.Entities.Models.Base;

namespace Web.Entities.Models.Korisnik
{
    public class KorisnikNotifikacija
    {

        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool Otvorena { get; set; }

        public string KorisnickoIme { get; set; }
        [JsonIgnore]
        [ForeignKey("KorisnickoIme")]
        public virtual Korisnik Korisnik { get; set; }

        public int NotifikacijaId { get; set; }
        [JsonIgnore]
        [ForeignKey("NotifikacijaId")]
        public virtual Notifikacija Notifikacija { get; set; }


    }
}
