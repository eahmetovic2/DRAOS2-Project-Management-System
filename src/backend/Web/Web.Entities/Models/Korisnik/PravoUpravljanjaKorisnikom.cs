using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Web.Entities.Models.Sifarnik;

namespace Web.Entities.Models.Korisnik
{
    public class PravoUpravljanjaKorisnikom
    {
        public int UlogaUpraviteljaId { get; set; }

        public int UlogaUpravljanogId { get; set; }

        [JsonIgnore]
        [ForeignKey("UlogaUpraviteljaId")]
        public virtual Uloga UlogaUpravitelja { get; set; }

        [JsonIgnore]
        [ForeignKey("UlogaUpravljanogId")]
        public virtual Uloga UlogaUpravljanog { get; set; }
    }
}
