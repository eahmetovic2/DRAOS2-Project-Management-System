using Web.Models.Response.Korisnik;

using System;
using System.Collections.Generic;
using System.Text;
using Web.Models.Response.Korisnik.Korisnik;

namespace Web.Models.Response.Token
{
    public class TokenModel
    {
        public Guid Id { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public DateTime DatumIsteka { get; set; }
        public String PosljednjiIp { get; set; }
        public String PosljedniKlijent { get; set; }
        public DateTime DatumZadnjeAkcije { get; set; }

        public KorisnikTokenModel Vlasnik { get; set; }
        public Core.Constants.TokenTip Tip { get; set; }
        public int UlogaId { get; set; }
        public string Uloga { get; set; }
        public string FrontendModul { get; set; }
        public string FrontendModulNaslov { get; set; }



    }
}
