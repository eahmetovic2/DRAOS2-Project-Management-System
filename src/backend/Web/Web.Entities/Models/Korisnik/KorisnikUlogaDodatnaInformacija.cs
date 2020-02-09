using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Web.Entities.Models.Sifarnik;

namespace Web.Entities.Models.Korisnik
{
    public class KorisnikUlogaDodatnaInformacija : BazniModel
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int KorisnikTipDodatneInformacijeId { get; set; }

        public int Vrijednost { get; set; }

        public int KorisnikUlogaId { get; set; }
        [JsonIgnore]
        public virtual KorisnikUloga KorisnikUloga { get; set; }

        [JsonIgnore]
        public virtual KorisnikTipDodatneInformacije KorisnikTipDodatneInformacije { get; set; }
    }
}
