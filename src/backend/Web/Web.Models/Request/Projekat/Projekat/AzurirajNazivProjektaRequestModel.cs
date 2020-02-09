using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Web.Models.Request.Base.Projekat
{
    public class AzurirajNazivProjektaRequestModel
    {
        [Required]
        [StringLength(128)]
        public String Naziv { get; set; }
    }
}
