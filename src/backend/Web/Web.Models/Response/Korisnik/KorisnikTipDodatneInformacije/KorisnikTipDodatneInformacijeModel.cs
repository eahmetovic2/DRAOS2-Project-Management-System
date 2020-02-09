using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models.Response.Korisnik.KorisnikTipDodatneInformacije
{
    public class KorisnikTipDodatneInformacijeModel
    {
        public int Id { get; set; }

        public string Sifra { get; set; }

        public string Opis { get; set; }

        public string Naziv { get; set; }

        public int Poredak { get; set; }

        public bool Onemogucen { get; set; }
    }
}
