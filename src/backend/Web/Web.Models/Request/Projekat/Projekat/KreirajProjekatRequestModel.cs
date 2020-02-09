using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Web.Models.Request.Base.Projekat
{
    public class KreirajProjekatRequestModel
    {
        [Required]
        [StringLength(128)]
        public String Naziv { get; set; }

        [StringLength(256)]
        public String Opis { get; set; }

    }

}
