using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Core.Constants;
using Web.Core.Database;
using Web.Entities;
using Web.Services.Definition.Base;

namespace Web.Services.Security.SecurityFilters.Projekat
{
    public class ProjekatSecurityFilter:SecurityFilter<Entities.Models.Projekat.Projekat>
    {
        private IAuthService authService;
        private IProjekatService projekatService;
        private Context context;

        public ProjekatSecurityFilter(IAuthService authService, IProjekatService projekatService, Context context)
        {
            this.authService = authService;
            this.projekatService = projekatService;
            this.context = context;
        }
        public override IQueryable<Entities.Models.Projekat.Projekat> Secure(IQueryable<Entities.Models.Projekat.Projekat> query, SecurityLevel securityLevel)
        {

            var korisnik = authService.TrenutniKorisnik();
            if (korisnik.TrenutnaUloga.VrijednostUAplikaciji != (int)Uloga.Administrator)
            {
                var ulogaId = korisnik.TrenutnaUloga.Id;

                var korisnikUlogaId = context.KorisnikUloge.Where(a => a.KorisnickoIme == korisnik.KorisnickoIme && a.UlogaId == ulogaId).Select(a => a.KorisnikUlogaId).FirstOrDefault();
                var korisnikProjekat = context.KorisnikProjekti.Where(p => p.ProjekatId ==9  && p.KorisnikUlogaId == korisnikUlogaId).AsQueryable().Count();
                //query = query.Where(p => korisnikProjekat.Contains(p.Id)).AsQueryable();
                
                if(korisnikProjekat==0)
                {
                    return Enumerable.Empty<Entities.Models.Projekat.Projekat>().AsQueryable();
                }
               
            }

            return query;
        }

        public IQueryable<Entities.Models.Projekat.Projekat> Secure1(int projekatId, SecurityLevel securityLevel)
        {

            var korisnik = authService.TrenutniKorisnik();
            if (korisnik.TrenutnaUloga.VrijednostUAplikaciji != (int)Uloga.Administrator)
            {
                var ulogaId = korisnik.TrenutnaUloga.Id;

                var korisnikUlogaId = context.KorisnikUloge.Where(a => a.KorisnickoIme == korisnik.KorisnickoIme && a.UlogaId == ulogaId).Select(a => a.KorisnikUlogaId).FirstOrDefault();
                var korisnikProjekat = context.KorisnikProjekti.Where(p => p.ProjekatId == projekatId && p.KorisnikUlogaId == korisnikUlogaId).AsQueryable().Count();


                if (korisnikProjekat == 0)
                {
                    return Enumerable.Empty<Entities.Models.Projekat.Projekat>().AsQueryable();
                }

            }
            var query = context.Projekti.Where(p =>p.Id==projekatId).AsQueryable();

            return query;
        }
    }
}
