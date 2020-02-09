using Web.Core.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Web.Entities.Models.Base
{
    public class LogAkcija
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(64)]
        public string KorisnickoIme { get; set; }

        public LogLevel Level { get; set; }

        public LogKategorija Kategorija { get; set; }
        //public int Akcija { get; set; }
        public Core.Constants.LogAkcija Akcija { get; set; }

        public string Opis { get; set; }

        [Required]
        public DateTime DatumAkcije { get; set; }

        [ForeignKey("KorisnickoIme")]
        [JsonIgnore]
        public virtual Korisnik.Korisnik Korisnik { get; set; }
    }
}
