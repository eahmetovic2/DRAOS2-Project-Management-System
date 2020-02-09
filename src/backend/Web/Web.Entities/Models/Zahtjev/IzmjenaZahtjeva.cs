using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Web.Entities.Models.Base;
using Web.Entities.Models.Projekat;

namespace Web.Entities.Models.Zahtjev
{
    public class IzmjenaZahtjeva:EntityAutoriziran
    {

        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public string DodijeljeniKorisnikIme { get; set; }
        [JsonIgnore]
        [ForeignKey("DodijeljeniKorisnikIme")]
        public Korisnik.Korisnik DodijeljeniKorisnik { get; set; }


        [JsonIgnore]
        [ForeignKey("NoviZahtjevStatusId")]
        public int NoviZahtjevStatusId { get; set; }
        public virtual Projekat.ZahtjevStatus NoviZahtjevStatus{get;set;}


        public int ZahtjevId { get; set; }
        [JsonIgnore]
        [ForeignKey("ZahtjevId")]
        public virtual Zahtjev Zahtjev { get; set; }
    }
}
