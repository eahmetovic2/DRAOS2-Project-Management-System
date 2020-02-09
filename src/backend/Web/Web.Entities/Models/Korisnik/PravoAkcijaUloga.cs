using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Entities.Models.Sifarnik;

namespace Web.Entities.Models.Korisnik
{
    public class PravoAkcijaUloga
    {
        public int PravoAkcijaId { get; set; }

        public int UlogaId { get; set; }

        [JsonIgnore]
        public virtual PravoAkcija PravoAkcija { get; set; }

        [JsonIgnore]
        public virtual Uloga Uloga { get; set; }
    }
}
