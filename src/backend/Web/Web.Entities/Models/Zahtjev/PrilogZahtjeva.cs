using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Web.Entities.Models.Zahtjev
{
    public class PrilogZahtjeva
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ZahtjevId { get; set; }
        [JsonIgnore]
        [ForeignKey("ZahtjevId")]
        public Zahtjev Zahtjev { get; set; }
             
        public int DokumentId { get; set; }
        [JsonIgnore]
        [ForeignKey("DokumentId")]
        public Dokument Dokument { get; set; }

    }
}
