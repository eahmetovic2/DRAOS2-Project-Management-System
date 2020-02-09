﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Entities.Models.Sifarnik
{
    public class LogLevel : Sifarnik
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Sifra { get; set; }

        [Required]
        [StringLength(1000)]
        public string Naziv { get; set; }
        
        public int? VrijednostUAplikaciji { get; set; }

        public int Poredak { get; set; }

        public bool IsDeleted { get; set; }
    }
}
