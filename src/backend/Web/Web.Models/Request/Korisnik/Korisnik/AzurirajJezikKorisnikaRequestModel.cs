using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Web.Models.Request.Korisnik.Korisnik
{
    public class AzurirajJezikKorisnikaRequestModel
    {
        [Required]
        public int? Id { get; set; }
    }
}
