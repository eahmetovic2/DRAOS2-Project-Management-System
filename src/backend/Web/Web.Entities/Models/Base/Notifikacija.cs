using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Web.Entities.Models.Base
{
   public class Notifikacija
    {

        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Naslov { get; set; }

        public string Poruka { get; set; }
        public DateTime? DatumKreiranja { get; set; }


        public int ProjekatId { get; set; }
        [JsonIgnore]
        [ForeignKey("ProjekatId")]
        public virtual Projekat.Projekat Projekat { get; set; }

        public int ZahtjevId { get; set; }
        [JsonIgnore]
        [ForeignKey("ZahtjevId")]
        public virtual Zahtjev.Zahtjev Zahtjev { get; set; }

    }
}
