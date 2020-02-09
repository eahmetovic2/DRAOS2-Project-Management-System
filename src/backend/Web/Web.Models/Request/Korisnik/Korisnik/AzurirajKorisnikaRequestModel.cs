
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Web.Models.Request.Korisnik.Korisnik;
using Web.Models.Request.Korisnik.Korisnik.Korisnik;

namespace Web.Models.Request.Korisnik
{
    public class AzurirajKorisnikaRequestModel
    {
        [Required]
        [StringLength(256)]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        [StringLength(128)]
        public String PunoIme { get; set; }

        public string Lozinka { get; set; }
        public int? PolId { get; set; }

        [Required]
        public List<AzurirajUlogeKorisnika> Uloge { get; set; }

        public List<AzurirajProjekteKorisnikaRequestModel> Projekti { get; set; }
        public List<AzuriranjeKategorijeKorisnikaRequestModel> Kategorije { get; set; }


    }
}
