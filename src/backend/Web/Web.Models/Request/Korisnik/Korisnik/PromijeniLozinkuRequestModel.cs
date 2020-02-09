using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Web.Models.Request.Korisnik
{
    public class PromijeniLozinkuRequestModel
    {
        [Required]
        public String Lozinka { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 6)]
        public String NovaLozinka { get; set; }
    }
}
