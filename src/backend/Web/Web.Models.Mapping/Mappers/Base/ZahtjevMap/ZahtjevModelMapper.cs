using Emis.Web.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities.Models.Base;
using Web.Entities.Models.Zahtjev;
using Web.Models.Response.Zahtjev;

namespace Web.Models.Mapping.Mappers.Base.ZahtjevMap
{
    public static class ZahtjevModelMapper
    {

        public static IQueryable<ZahtjevModel> ToZahtjevModel(this IQueryable<Zahtjev> query)
        {
            return query.Select(zahtjev => new ZahtjevModel()
            {
                Id = zahtjev.Id,
                Naziv = zahtjev.Naziv,
                Opis = zahtjev.Opis,
                ZahtjevKategorija = zahtjev.ZahtjevKategorija.Naziv,
                ZahtjevPrioritet = zahtjev.ZahtjevPrioritet.Naziv,
                ZahtjevStatus = new ZahtjevStatusModel { Id = zahtjev.ZahtjevStatus.Id, Naziv = zahtjev.ZahtjevStatus.Naziv, Oznaka = zahtjev.ZahtjevStatus.Oznaka },
                ZahtjevTip = zahtjev.ZahtjevTip.Naziv,
                DioProjekta = zahtjev.ZahtjevKategorija.DioProjekta.Naziv,
                CreatedBy = zahtjev.CreatedBy,
                ProjekatId = zahtjev.ProjekatId,
                DatumKreiranja = zahtjev.DatumKreiranja,
                DatumIzmjene = zahtjev.DatumIzmjene,
                DodijeljeniKorisnikIme=zahtjev.DodijeljeniKorisnikIme ?? null,
                PotrosenoVrijeme=TimeSpan.FromSeconds(zahtjev.PotrošenoVrijeme ?? 0).ToString(@"dd\:hh\:mm\:ss")

        });
        }

        public static ZahtjevModel ToZahtjevModel(this Zahtjev zahtjev)
        {
            return new ZahtjevModel()
            {
                Id = zahtjev.Id,
                Naziv = zahtjev.Naziv,
                Opis = zahtjev.Opis
            };
        }

        public static IQueryable<ZahtjevListModelItem> ToZahtjevListModelItem(this IQueryable<Zahtjev> query)
        {
            return query.Select(zahtjev => new ZahtjevListModelItem()
            {
                Id = zahtjev.Id,
                Naziv = zahtjev.Naziv,
                Opis = zahtjev.Opis,
                ZahtjevKategorija = zahtjev.ZahtjevKategorija.Naziv,
                ZahtjevPrioritet = zahtjev.ZahtjevPrioritet.Naziv,
                ZahtjevStatus = zahtjev.ZahtjevStatus.Naziv,
                ZahtjevTip = zahtjev.ZahtjevTip.Naziv,
                DatumKreiranja = zahtjev.DatumKreiranja,
                CreatedBy = zahtjev.CreatedBy,
                Projekat = zahtjev.Projekat.Naziv,
                DioProjekta = zahtjev.ZahtjevKategorija.DioProjekta.Naziv


                /*Uloga = string.Join(", ", zahtjev.ZahtjevKategorije
                                                  .Select(a => a.)
                                                  .ToList()),*/
                /*DodijeljeniKorisnikIme=zahtjev.DodijeljeniKorisnikIme,
                EstimiranoVrijeme=zahtjev.EstimiranoVrijeme,
                PotrošenoVrijeme=zahtjev.PotrošenoVrijeme,*/

            });
        }


    }
}
