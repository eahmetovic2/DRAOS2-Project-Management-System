﻿using System;
using System.Collections.Generic;
using System.Text;
using Web.Models.Response.Korisnik.KorisnikDodatneInformacije;
using Web.Models.Response.Korisnik.PravoAkcija;

namespace Web.Models.Response.Korisnik.Korisnik
{
    public class KorisnikTokenModel : BazniResponseModel
    {
        public String KorisnickoIme { get; set; }
        public String Email { get; set; }

        public int TrenutnaUlogaId { get; set; }

        public String PunoIme { get; set; }

        public DateTime? DatumKreiranja { get; set; }

        public bool Onemogucen { get; set; }

        public KorisnikDodatneInformacijeListModel DodatneInformacije { get; set; }

        public PravoAkcijaListModel DozvoljeneAkcije { get; set; }

        public int? Jezik { get; set; }
    }
}
