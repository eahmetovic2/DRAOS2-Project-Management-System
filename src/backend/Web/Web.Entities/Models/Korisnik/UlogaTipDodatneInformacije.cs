using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Entities.Models.Sifarnik;

namespace Web.Entities.Models.Korisnik
{
    public class UlogaTipDodatneInformacije
    {
        public int UlogaId { get; set; }

        public int KorisnikTipDodatneInformacijeId { get; set; }

        [JsonIgnore]
        public virtual Uloga Uloga { get; set; }

        [JsonIgnore]
        public virtual KorisnikTipDodatneInformacije KorisnikTipDodatneInformacije { get; set; }
    }
}
