using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities;
using Web.Entities.Models.Projekat;
using Web.Models.Mapping.Mappers.Base.ProjekatMap;
using Web.Models.Request.Projekat.ZahtjevKategorija;
using Web.Models.Response.Projekat.ZahtjevKategorija;
using Web.Services.Definition.Projekat;
using Web.Services.Result;

namespace Web.Services.Implementation.Base
{
    public class ZahtjevKategorijaService:Service,IZahtjevKategorijaService
    {

        /// <summary>
        /// Entity framework db kontekst 
        /// </summary>
        private Context context;


        private IAuthService authService;

        /// <summary>
        /// 
        /// </summary>


        private IApplicationConfigurationService applicationConfigurationService;


        /// <summary>
        /// Konstruktor servisa
        /// </summary>
        public ZahtjevKategorijaService(ILifetimeScope scope, Context context, IAuthService authService, IApplicationConfigurationService applicationConfigurationService)
            : base(scope)
        {
            this.context = context;
            this.authService = authService;
            this.applicationConfigurationService = applicationConfigurationService;

        }

        public ServiceResult<ZahtjevKategorija> DodajNovuKategorijuZahtjevaDijelaProjekta(int dioProjektaId, KreirajZahtjevKategorijaRequestModel zahtjevKategorijaModel)
        {
            if (zahtjevKategorijaModel.Naziv.Length > 128)
                return Error("Naziv ne može biti veći od 128 karaktera");

            ZahtjevKategorija zahtjevKategorija = new ZahtjevKategorija();
            zahtjevKategorija.Naziv = zahtjevKategorijaModel.Naziv;
            zahtjevKategorija.DioProjektaId = dioProjektaId;

            context.Add(zahtjevKategorija);

            SaveChanges(context);


            return Ok(zahtjevKategorija);
        }

        public ServiceResult<List<ZahtjevKategorijaModel>> VratiSveKategorijeKorisnika(string korisnickoIme)
        {
            var zahtjevKategorije = context.ZahtjevKategorije
                            .Where(z => z.KorisnikKategorije.Any(k=>k.KorisnickoIme==korisnickoIme)).ToZahtjevKategorijaModel().ToList();
            if (zahtjevKategorije == null)
                return NotFound();
            return Ok(zahtjevKategorije);
        }

        public ServiceResult<List<ZahtjevKategorijaModel>> VratiSveKategorijeZahtjevaDijelaProjekta(int dioProjektaId)
        {

            var zahtjevKategorije = context.ZahtjevKategorije
                .Where(p => p.DioProjektaId == dioProjektaId).ToZahtjevKategorijaModel().ToList();
            if (zahtjevKategorije == null)
                return NotFound();

            return Ok(zahtjevKategorije);
        }
    }
}

