using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Web.Entities.Models.Sifarnik
{
    public class Prevod : BazniModel
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String Tabela { get; set; }
        public int RedId { get; set; }
        public String Polje { get; set; }
        public int Jezik { get; set; }
        public String PrevodTekst { get; set; }

    }
}
