using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models.Response.Projekat.ProjekatKonfiguracija
{
    public class ProjekatKonfiguracijaModel
    {
        public int Id { get; set; }

        public TimeSpan RadnoVrijemeOd { get; set; }

        public TimeSpan RadnoVrijemeDo { get; set; }

        public String RadniDani { get; set; }
    }
}
