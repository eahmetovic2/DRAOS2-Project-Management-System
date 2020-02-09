using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Entities.Models.Sifarnik
{
    public class FrontendModul : BazniModel
    {
        public int Id { get; set; }

        public string Naziv { get; set; }

        public string Sifra { get; set; }

        public bool Onemogucen { get; set; }

        [JsonIgnore]
        public virtual ICollection<Uloga> Uloge { get; set; }
    }
}
