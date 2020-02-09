using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities.Models.Base;
using Web.Models.Response.LogAkcija;

namespace Web.Models.Mapping.Mappers.Base.LogAkcijaMap
{
    public static class LogAkcijaMapper
    {
        public static IQueryable<LogAkcijaModel> ToLogAkcijaModel(this IQueryable<LogAkcija> query)
        {
            return query.Select(akcija => new LogAkcijaModel()
            {
                Id = akcija.Id,
                Akcija = akcija.Akcija.ToString(),
                Level = akcija.Level.ToString(),
                Kategorija = akcija.Kategorija.ToString(),
                KorisnickoIme = akcija.KorisnickoIme,
                DatumAkcije = akcija.DatumAkcije,
                Opis = akcija.Opis
            });
        }

        public static IQueryable<LogAkcijaListModelItem> ToLogAkcijaListModelItem(this IQueryable<LogAkcija> query)
        {
            return query.Select(akcija => new LogAkcijaListModelItem()
            {
                Id = akcija.Id,
                Akcija = akcija.Akcija.ToString(),
                Level = akcija.Level.ToString(),
                Kategorija = akcija.Kategorija.ToString(),
                KorisnickoIme = akcija.KorisnickoIme,
                DatumAkcije = akcija.DatumAkcije,
                Opis = akcija.Opis
            });
        }
    }
}