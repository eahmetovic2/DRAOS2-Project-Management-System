using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Entities.Models.Korisnik;

namespace Web.Entities.Models.Sifarnik
{
    public class Uloga : Sifarnik
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Sifra { get; set; }

        [Required]
        [StringLength(1000)]
        public string Naziv { get; set; }
        
        public int? VrijednostUAplikaciji { get; set; }

        public int Poredak { get; set; }

        public bool IsDeleted { get; set; }

        public int FrontendModulId { get; set; }

        [JsonIgnore]
        public virtual FrontendModul FrontendModul { get; set; }

        [JsonIgnore]
        public virtual ICollection<PravoAkcijaUloga> PravoAkcijaUloge { get; set; }

        [JsonIgnore]
        public virtual ICollection<UlogaTipDodatneInformacije> TipoviDodatneInformacije { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(PravoUpravljanjaKorisnikom.UlogaUpravitelja))]
        public virtual ICollection<PravoUpravljanjaKorisnikom> PravaUpravljanja { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(PravoUpravljanjaKorisnikom.UlogaUpravljanog))]
        public virtual ICollection<PravoUpravljanjaKorisnikom> PravaUpravljanosti { get; set; }

        [JsonIgnore]
        public virtual ICollection<KorisnikUloga> KorisnikUloge { get; set; }

        [JsonIgnore]
        public virtual ICollection<Token> Tokeni { get; set; }
    }
}
