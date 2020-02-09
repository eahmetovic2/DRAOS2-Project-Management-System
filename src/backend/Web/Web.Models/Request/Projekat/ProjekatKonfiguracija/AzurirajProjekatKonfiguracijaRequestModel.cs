using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Web.Models.Request.Base.Projekat
{
    public class AzurirajProjekatKonfiguracijaRequestModel
    {
        public int Id { get; set; }


        public TimeSpan RadnoVrijemeOd { get; set; }

        public TimeSpan RadnoVrijemeDo { get; set; }

        public String RadniDani { get; set; }
    }
}
