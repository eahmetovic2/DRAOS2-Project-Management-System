using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities.Models.Projekat;
using Web.Models.Response.Projekat.ZahtjevKategorija;

namespace Web.Models.Mapping.Mappers.Base.ProjekatMap
{
    public static class ZahtjevKategorijaModelMapper
    {
        public static IQueryable<ZahtjevKategorijaModel> ToZahtjevKategorijaModel(this IQueryable<ZahtjevKategorija> query)
        {
            return query.Select(zahtjevStatus => new ZahtjevKategorijaModel()
            {
                Id = zahtjevStatus.Id,
                Naziv=zahtjevStatus.Naziv,
                DetaljanNaziv = zahtjevStatus.Naziv +" (Projekat: "+ zahtjevStatus.DioProjekta.Projekat.Naziv+", "+"Dio projekta: "+ zahtjevStatus.DioProjekta.Naziv+")"
            });
        }

        public static ZahtjevKategorijaModel ToZahtjevKategorijaModel(ZahtjevKategorija query)
        {
            ZahtjevKategorijaModel zahtjevKategorijaModel =
            new ZahtjevKategorijaModel()
            {
                Id = query.Id,
                Naziv = query.Naziv
            };
            return zahtjevKategorijaModel;
        }
    }
}
