using System;
using System.Collections.Generic;
using System.Text;
using Web.Models.Response.Korisnik.KorisnikTipDodatneInformacije;

namespace Web.Models.Response.Korisnik.KorisnikDodatneInformacije
{
    public class KorisnikDodatneInformacijeListModelItem
    {
        public string KorisnickoIme { get; set; }

        public KorisnikTipDodatneInformacijeModel TipDodatneInformacije { get; set; }

        public int Vrijednost { get; set; }
    }
}
