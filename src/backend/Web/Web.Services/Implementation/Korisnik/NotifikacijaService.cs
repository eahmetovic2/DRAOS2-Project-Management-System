using Autofac;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Entities;
using Web.Entities.Models.Base;
using Web.Entities.Models.Korisnik;
using Web.Models.Request.Base.Notifikacija;
using Web.Services.Definition.Korisnik;
using Web.Services.Result;

namespace Web.Services.Implementation.Korisnik
{
    public class NotifikacijaService : Service, INotifikacijaService
    {

        private Context context;


        private IAuthService authService;


        private IApplicationConfigurationService applicationConfigurationService;

        public NotifikacijaService(ILifetimeScope scope, Context context, IAuthService authService, IApplicationConfigurationService applicationConfigurationService)
            : base(scope)
        {
            this.context = context;
            this.authService = authService;
            this.applicationConfigurationService = applicationConfigurationService;

        }
        public ServiceResult<List<Notifikacija>> VratiNotifikacijeZaKorisnika()
        {

            var trenutni = authService.TrenutniKorisnik();

            //vraca sve neotvorene notifikacije
            var notifikacije = context.KorisnikNotifikacije.Include(n => n.Notifikacija).Where(k => k.KorisnickoIme == trenutni.KorisnickoIme && k.Otvorena == false).Select(n => n.Notifikacija).OrderByDescending(k => k.DatumKreiranja).ToList();

            return Ok(notifikacije);
        }

        public ServiceResult<Nothing> OtvoriNotifikaciju(OtvoriNotifikacijuRequestModel model)
        {
            var trenutni = authService.TrenutniKorisnik();

            var korisnikNotifikacija = context.KorisnikNotifikacije.Include(k => k.Notifikacija).Where(k => k.NotifikacijaId == model.NotifikacijaId).First();

            var korisnikNotifikacijeZahtjeva = context.KorisnikNotifikacije
                .Where(k => k.Notifikacija.ZahtjevId == korisnikNotifikacija.Notifikacija.ZahtjevId
                && k.KorisnickoIme == korisnikNotifikacija.KorisnickoIme
                && k.Otvorena == false).ToList();

            foreach (var k in korisnikNotifikacijeZahtjeva)
            {
                k.Otvorena = true;
            }

            SaveChanges(context);


            return Ok();
        }

        public ServiceResult<Nothing> OtvoriKorisnikoveNotifikacijeZahtjeva(int zahtjevId)
        {
            var trenutni = authService.TrenutniKorisnik();

            var korisnikNotifikacije = context.KorisnikNotifikacije.Include(k => k.Notifikacija)
                .Where(k => k.Notifikacija.ZahtjevId == zahtjevId 
                && k.KorisnickoIme==trenutni.KorisnickoIme
                && k.Otvorena==false).ToList();


            foreach (var k in korisnikNotifikacije)
            {
                k.Otvorena = true;
            }

            SaveChanges(context);


            return Ok();
        }



    }
}
