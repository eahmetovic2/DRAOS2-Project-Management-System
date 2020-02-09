using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Web.Entities.Models.Projekat
{
    public class ZahtjevTip
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Naziv { get; set; }

        public bool Default { get; set; }

        public int ProjekatId { get; set; }
        [JsonIgnore]
        [ForeignKey("ProjekatId")]
        public virtual Projekat Projekat { get; set; }
    }
}
