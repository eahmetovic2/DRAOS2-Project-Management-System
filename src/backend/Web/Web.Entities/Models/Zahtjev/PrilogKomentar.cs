using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Web.Entities.Models.Zahtjev
{
    public class PrilogKomentar
    {

        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ZahtjevKomentarId { get; set; }
        [JsonIgnore]
        [ForeignKey("ZahtjevKomentarId")]
        public ZahtjevKomentar ZahtjevKomentar { get; set; }

        public int DokumentId { get; set; }
        [JsonIgnore]
        [ForeignKey("DokumentId")]
        public Dokument Dokument { get; set; }


    }
}
