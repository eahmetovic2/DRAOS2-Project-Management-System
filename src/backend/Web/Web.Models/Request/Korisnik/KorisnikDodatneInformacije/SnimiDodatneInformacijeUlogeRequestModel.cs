﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models.Request.Korisnik.KorisnikDodatneInformacije
{
    public class SnimiDodatneInformacijeUlogeRequestModel
    {
        public int UlogaId { get; set; }

        public List<int> DodatneInformacije { get; set; }
    }
}
