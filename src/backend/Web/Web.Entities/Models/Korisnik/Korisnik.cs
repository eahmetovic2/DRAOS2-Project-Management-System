using Web.Core.Constants;
using Web.Entities.Models.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Web.Entities.Models.Sifarnik;

namespace Web.Entities.Models.Korisnik
{
    /// <summary>
    /// Korisnika aplikacije
    /// </summary>
    public class Korisnik : EntityAutoriziran
    {
        /// <summary>
        /// Korisnicko ime korisnika
        /// </summary>
        [Key, Column(Order = 0)]        
        [Required]
        [StringLength(64)]
        public String KorisnickoIme { get; set; }

        /// <summary>
        /// Email adresa korisnika
        /// </summary>
        [Required]
        [StringLength(256)]
        public String Email { get; set; }

        /// <summary>
        /// Puno ime korisnika
        /// </summary>
        [Required]
        [StringLength(128)]
        public String PunoIme { get; set; }

        /// <summary>
        /// Pol
        /// </summary> 
        public int? PolId { get; set; }
        [JsonIgnore]
        public virtual Pol Pol { get; set; }

        /// <summary>
        /// Tajna korisnika za prijavu (obicno hash+salt)
        /// </summary>
        public byte[] Tajna { get; set; }

        /// <summary>
        /// Da li je korisnik onemogucen
        /// </summary>
        public bool Onemogucen { get; set; }

		public bool EmailVerifikovan { get; set; }

		public string BrojMobitela { get; set; }

        public int? Jezik { get; set; }

        [JsonIgnore]
        public virtual ICollection<Base.LogAkcija> LogAkcije { get; set; }

        [JsonIgnore]
        public virtual ICollection<KorisnikUloga> KorisnikUloge { get; set; }


        [NotMapped]
        public int TrenutnaUlogaId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public Sifarnik.Uloga TrenutnaUloga { get; set; }
    }    
}
