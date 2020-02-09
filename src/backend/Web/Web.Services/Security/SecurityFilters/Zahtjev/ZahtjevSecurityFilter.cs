using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Core.Constants;
using Web.Core.Database;
using Web.Entities;
using Web.Services.Definition.Base;

namespace Web.Services.Security.SecurityFilters.Zahtjev
{
    public class ZahtjevSecurityFilter: SecurityFilter<Entities.Models.Zahtjev.Zahtjev>
    {

        private IAuthService authService;
        private IZahtjevService zahtjevService;
        private Context context;

        public ZahtjevSecurityFilter(IAuthService authService, IZahtjevService zahtjevService, Context context)
        {
            this.authService = authService;
            this.zahtjevService = zahtjevService;
            this.context = context;
        }
        public override IQueryable<Entities.Models.Zahtjev.Zahtjev> Secure(IQueryable<Entities.Models.Zahtjev.Zahtjev> query, SecurityLevel securityLevel)
        {

            var korisnik = authService.TrenutniKorisnik();
            if (korisnik.TrenutnaUloga.VrijednostUAplikaciji != (int)Uloga.Administrator)
            {
                var ulogaId = korisnik.TrenutnaUloga.Id;

                var korisnikUlogaId = context.KorisnikUloge.Where(a => a.KorisnickoIme == korisnik.KorisnickoIme && a.UlogaId == ulogaId).Select(a => a.KorisnikUlogaId).FirstOrDefault();
                var korisnikProjekat = context.KorisnikProjekti.Where(p => p.ProjekatId == query.ElementAt(0).Id && p.KorisnikUlogaId == korisnikUlogaId).Select(z => z.ProjekatId);
                query = query.Where(p => korisnikProjekat.Contains(p.Id)).AsQueryable();


            }

            return query;
        }
    }
}
