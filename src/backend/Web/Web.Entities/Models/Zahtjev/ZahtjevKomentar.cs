using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Web.Entities.Models.Base;

namespace Web.Entities.Models.Zahtjev
{
    public class ZahtjevKomentar:EntityAutoriziran
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Komentar { get; set; }

        public int ZahtjevId { get; set; }
        [JsonIgnore]
        [ForeignKey("ZahtjevId")]
        public virtual Zahtjev Zahtjev { get; set; }
    }
}
