
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Web.Models.Request.Korisnik.Korisnik;
using Web.Models.Request.Korisnik.Korisnik.Korisnik;

namespace Web.Models.Request.Korisnik
{
    public class KreirajKorisnikaRequestModel
    {
        [Required]
        [StringLength(64)]
        public String KorisnickoIme { get; set; }

        [Required]
        [StringLength(256)]
        public String Email { get; set; }

        [Required]
        [StringLength(128)]
        public String PunoIme { get; set; }

        [Required]
        [StringLength(128)]
        public String Lozinka { get; set; }
        public int? PolId { get; set; }

        public int SkolaId { get; set; }

        public int OsobaId { get; set; }

        [Required]
        public List<DodavanjeUlogeKorisnika> Uloge { get; set; }

        public List<DodavanjeKorisnikaNaProjekat> Projekti { get; set; }

        public List<DodavanjeKategorijeKorisnika> Kategorije { get; set; }



    }
}
