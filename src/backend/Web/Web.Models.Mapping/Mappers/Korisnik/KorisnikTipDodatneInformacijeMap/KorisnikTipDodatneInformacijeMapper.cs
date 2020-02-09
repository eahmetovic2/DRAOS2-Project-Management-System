﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities.Models.Sifarnik;
using Web.Models.Response.Korisnik.KorisnikTipDodatneInformacije;

namespace Web.Models.Mapping.Mappers.Korisnik.KorisnikTipDodatneInformacijeMap
{
    public static class KorisnikTipDodatneInformacijeMapper
    {
        public static IQueryable<KorisnikTipDodatneInformacijeModel> ToKorisnikTipDodatneInformacijeModel(this IQueryable<KorisnikTipDodatneInformacije> query)
        {
            return query.Select(value => new KorisnikTipDodatneInformacijeModel
            {
                Id = value.Id,
                Naziv = value.Naziv,
                Onemogucen = value.Onemogucen,
                Opis = value.Opis,
                Poredak = value.Poredak,
                Sifra = value.Sifra
            });
        }
    }
}
