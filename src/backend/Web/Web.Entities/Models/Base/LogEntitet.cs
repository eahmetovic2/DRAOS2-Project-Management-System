using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Web.Entities.Models.Base
{
    public class LogEntitet
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(64)]
        public string KorisnickoIme { get; set; }

        public int EntitetId { get; set; }

        public Web.Core.Constants.LogEntitet Entitet { get; set; }

        public string Vrijednost { get; set; }

        [Required]
        public DateTime DatumAkcije { get; set; }

        [ForeignKey("KorisnickoIme")]
        [JsonIgnore]
        public virtual Korisnik.Korisnik Korisnik { get; set; }
    }
}
