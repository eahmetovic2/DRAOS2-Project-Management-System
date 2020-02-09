using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Web.Entities.Models.Projekat
{
    public class ProjekatKonfiguracija
    {

        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public TimeSpan RadnoVrijemeOd { get; set; }

        public TimeSpan RadnoVrijemeDo { get; set; }

        public String RadniDani { get; set; }
    }
}
