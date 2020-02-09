using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities;
using Web.Entities.Models.Korisnik;
using Web.Models.Mapping.Mappers.Korisnik.PravoUpravljanjaKorisnikomMap;
using Web.Models.Response.Korisnik.PravoUpravljanjaKorisnikom;
using Web.Services.Definition.Korisnik;
using Web.Services.Result;

namespace Web.Services.Implementation.Korisnik
{
    public class PravoUpravljanjaKorisnikomService : Service, IPravoUpravljanjaKorisnikomService
    {
        Context context;

        public PravoUpravljanjaKorisnikomService(ILifetimeScope scope, Context context) : base(scope)
        {
            this.context = context;
        }

        public ServiceResult<PravoUpravljanjaKorisnikomListModel> VratiSve(int ulogaId)
        {
            var dozvoljeneUloge = VratiPravaUpravljanjaKorisnikom(ulogaId)
                                    .ToPravoUpravljanjaKorisnikomListModelItem()
                                    .ToList();

            var result = new PravoUpravljanjaKorisnikomListModel
            {
                Items = dozvoljeneUloge
            };

            return Ok(result);
        }

        public IQueryable<PravoUpravljanjaKorisnikom> VratiPravaUpravljanjaKorisnikom(int ulogaId)
        {
            return context.PravaUpravljanjaKorisnicima
                            .Where(a => a.UlogaUpraviteljaId == ulogaId);
        }
    }
}
