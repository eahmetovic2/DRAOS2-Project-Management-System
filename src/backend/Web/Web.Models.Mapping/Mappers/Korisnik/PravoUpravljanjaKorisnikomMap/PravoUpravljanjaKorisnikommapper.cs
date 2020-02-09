using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities.Models.Korisnik;
using Web.Models.Response.Korisnik.PravoUpravljanjaKorisnikom;

namespace Web.Models.Mapping.Mappers.Korisnik.PravoUpravljanjaKorisnikomMap
{
    public static class PravoUpravljanjaKorisnikomMapper
    {
        public static IQueryable<PravoUpravljanjaKorisnikomListModelItem> ToPravoUpravljanjaKorisnikomListModelItem(this IQueryable<PravoUpravljanjaKorisnikom> query)
        {
            return query.Select(value => new PravoUpravljanjaKorisnikomListModelItem
            {
                Id = value.UlogaUpravljanogId,
                Uloga = value.UlogaUpravljanog.Naziv,
                PotrebneDodatneInformacije = value.UlogaUpravljanog.TipoviDodatneInformacije.Select(a => a.KorisnikTipDodatneInformacije.Sifra).ToList()
            });
        }
    }
}
