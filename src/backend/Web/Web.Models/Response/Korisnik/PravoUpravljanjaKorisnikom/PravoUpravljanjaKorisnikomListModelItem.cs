﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models.Response.Korisnik.PravoUpravljanjaKorisnikom
{
    public class PravoUpravljanjaKorisnikomListModelItem
    {
        public int Id { get; set; }
        public string Uloga { get; set; }
        public List<string> PotrebneDodatneInformacije { get; set; }
    }
}
