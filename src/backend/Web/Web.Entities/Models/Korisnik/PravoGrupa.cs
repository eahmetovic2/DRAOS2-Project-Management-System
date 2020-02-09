using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Web.Entities.Models.Korisnik
{
    public class PravoGrupa : BazniModel
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(45)]
        public string Sifra { get; set; }

        [StringLength(200)]
        public string Naziv { get; set; }

        [StringLength(200)]
        public string Opis { get; set; }

        public virtual ICollection<PravoObjekt> PravoObjekti { get; set; }
    }
}
