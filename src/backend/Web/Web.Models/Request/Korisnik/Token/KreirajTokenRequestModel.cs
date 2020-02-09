using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Web.Models.Request.Token
{
    public class KreirajTokenRequestModel
    {
        [Required]
        [StringLength(256)]
        public String Lozinka { get; set; }

        [Required]
        public int UlogaId { get; set; }
    }
}
