using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Web.Entities.Models.Korisnik
{
    public class PrebacivanjePrava
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string OdKorisnickogImena { get; set; }
        [JsonIgnore]
        [ForeignKey("OdKorisnickogImena")]
        public virtual Korisnik OdKorisnika { get; set; }

        public string DoKorisnickogImena { get; set; } 
        [JsonIgnore]
        [ForeignKey("DoKorisnickogImena")]
        public virtual Korisnik DoKorisnika { get; set; }

        public DateTime VrijemeOd { get; set; }
        public DateTime VrijemeDo { get; set; }
    }
}
