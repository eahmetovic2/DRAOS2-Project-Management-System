using Autofac;
using Web.Core.Auth;
using Web.Entities;
using Web.Entities.Models.Korisnik;
using Web.Models.Request.Korisnik;
using Web.Models.Response.Korisnik;
using Web.Models.Response.Token;
using Web.Services.Result;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Web.Core.Database;
using Web.Models.Mapping.Mappers.KorisnikMap;
using Web.Models.Response.Korisnik.Korisnik;
using Web.Services.Definition.Korisnik;
using Web.Core.Constants;
using Web.Models.Request.Korisnik.Korisnik;
using Web.Entities.Models.Projekat;
using Web.Models.Mapping.Mappers.Korisnik.KorisnikZahtjevKategorijaMap;
using Web.Models.Mapping.Mappers.Base.ProjekatMap;

namespace Web.Services.Implementation
{
    /// <summary>
    /// Implementacija servisa koji radi sa korisnicima
    /// </summary>
    public class KorisnikService : Service, IKorisnikService
    {
        /// <summary>
        /// Entity framework db kontekst 
        /// </summary>
        private Context context;

        /// <summary>
        /// 
        /// </summary>
        private IAuthService authService;

        /// <summary>
        /// 
        /// </summary>


        private IApplicationConfigurationService applicationConfigurationService;
        private IPravoUpravljanjaKorisnikomService pravoUpravljanjaKorisnikomService;


        /// <summary>
        /// Konstruktor servisa
        /// </summary>
        public KorisnikService(ILifetimeScope scope, Context context, IAuthService authService, IApplicationConfigurationService applicationConfigurationService, IPravoUpravljanjaKorisnikomService pravoUpravljanjaKorisnikomService)
            : base(scope)
        {
            this.context = context;
            this.authService = authService;
            this.applicationConfigurationService = applicationConfigurationService;
            this.pravoUpravljanjaKorisnikomService = pravoUpravljanjaKorisnikomService;

        }

        public ServiceResult<KorisnikModel> VratiKorisnikaPoKorisnickomImenu(String korisnickoIme)
        {
            var securityLevel = new SecurityLevel();

            var trenutniKorisnik = authService.TrenutniKorisnik();

            if(trenutniKorisnik!=null && trenutniKorisnik.TrenutnaUloga.VrijednostUAplikaciji != (int)Uloga.Administrator && trenutniKorisnik.TrenutnaUloga.VrijednostUAplikaciji != (int)Uloga.Moderator && trenutniKorisnik.KorisnickoIme!=korisnickoIme)
            {
                return Error("Nemate pravo pristupa");
            }
            // dobavi razred ako postoji
            var korisnici = Secure(context.Korisnici
                                //.Include(x => x.KorisnikUloge).ThenInclude(x => x.KorisnikProjekti).ThenInclude(x => x.KorisnikKategorije).ThenInclude(x=>x.ZahtjevKategorija)
                                .Include(x => x.KorisnikUloge).ThenInclude(x => x.KorisnikProjekti).ThenInclude(x => x.Projekat)
                                    .Include(x => x.KorisnikUloge).ThenInclude(x => x.Uloga)
                                    .ThenInclude(x => x.TipoviDodatneInformacije)
                                    .ThenInclude(x => x.KorisnikTipDodatneInformacije)
                                    .Include(x=>x.KorisnikUloge).ThenInclude(x=>x.KorisnikProjekti).ThenInclude(x=>x.KorisnikKategorije)
                                .Where(k => k.KorisnickoIme == korisnickoIme).AsQueryable(), securityLevel);

            var korisnik = korisnici
                            .SingleOrDefault(k => k.KorisnickoIme == korisnickoIme);

            //korisnik.Projekti= context.KorisnikProjekti.Where(korisnik.Projekti.Contains(korisnici.ToKorisnikModel())
            foreach (var uloga in korisnik.KorisnikUloge)
            {
                uloga.KorisnikProjekti = context.KorisnikProjekti.Where(x => x.KorisnikUlogaId == uloga.KorisnikUlogaId).ToList();

            }



            // dobavi korisnika ako postoji
            if (korisnik == null)
                return NotFound();

            var result = korisnik.ToKorisnikModel();

            /* var korisnikProjekti = context.KorisnikProjekti.Include(x => x.Projekat).Where(c => korisnik.KorisnikUloge.Contains(c.KorisnikUloga)).Select(p => new KorisnikProjekatModel
             {
                 Id = p.Id,
                 Naziv = p.Projekat.Naziv

             }).ToList();
             result.Projekti.AddRange(korisnikProjekti);*/


            return Ok(result);
        }

        public ServiceResult<KorisnikModel> AzurirajKorisnika(String korisnickoIme, AzurirajKorisnikaRequestModel model)
        {
            var securityLevel = new SecurityLevel();

            var korisnik = Secure(context.Korisnici.Include(k => k.KorisnikUloge).AsQueryable(), securityLevel)
                            .SingleOrDefault(k => k.KorisnickoIme == korisnickoIme);

            /*context.Korisnici.AsQueryable()
                            .SingleOrDefault(k => k.KorisnickoIme == korisnickoIme);*/
            if (korisnik == null)
                return NotFound();

            var trenutni = authService.TrenutniKorisnik();


            //za sada je podeseno da može imati jednu ulogu samo
            var korisnikUlogaId = korisnik.KorisnikUloge.Select(k => k.KorisnikUlogaId).FirstOrDefault();


            var korisnikProjektiKorisnika = context.KorisnikProjekti.Where(k => k.KorisnikUlogaId == korisnikUlogaId).ToList();

            var vrijednostUAplikaciji = context.Uloge.Where(u => u.Id == model.Uloge[0].VrstaUloge.Id).Select(u => u.VrijednostUAplikaciji).FirstOrDefault();

            if (vrijednostUAplikaciji == (int)Uloga.Support)
            {
                var kategorije = new List<int>();

                foreach (var kat in model.Kategorije)
                {
                    kategorije.Add(kat.Id);
                }

                var kategorijeKorisnika = context.KorisnikKategorije.Include(z => z.ZahtjevKategorija).Where(k => korisnickoIme == k.KorisnickoIme).ToList();

                var obrisatiKategorijeKorisnika = context.KorisnikKategorije.Include(z => z.ZahtjevKategorija).Where(k => !kategorije.Contains(k.ZahtjevKategorijaId) && korisnickoIme == k.KorisnickoIme).Distinct();
                var dodatiKategorijeKorisnika = kategorije.Where(p => !kategorijeKorisnika.Any(a => a.ZahtjevKategorijaId == p)).ToList();

                context.KorisnikKategorije.RemoveRange(obrisatiKategorijeKorisnika);
                context.KorisnikKategorije.AddRange(dodatiKategorijeKorisnika.Select(n => new KorisnikKategorija
                {
                    KorisnickoIme = korisnickoIme,
                    ZahtjevKategorijaId = n
                }));

                var noviProjekti = context.ZahtjevKategorije.Where(z => kategorije.Contains(z.Id)).Select(z => z.DioProjekta.ProjekatId).Distinct();

                var obrisatiProjekte = korisnikProjektiKorisnika.Where(p => !noviProjekti.Any(a => a == p.ProjekatId)).ToList();


                var dodatiProjekte = noviProjekti.Where(p => !korisnikProjektiKorisnika.Any(a => a.ProjekatId == p)).ToList();

                context.KorisnikProjekti.RemoveRange(obrisatiProjekte);

                context.KorisnikProjekti.AddRange(dodatiProjekte.Select(n => new KorisnikProjekat
                {
                    ProjekatId = n,
                    KorisnikUlogaId = korisnikUlogaId
                }));

            }
            else
            {

                var obrisatiKorisnikProjekte = korisnikProjektiKorisnika.Where(p => !model.Projekti.Any(a => a.Id == p.ProjekatId)).ToList();

                var dodatiKorisnikProjekte = model.Projekti.Where(p => !korisnikProjektiKorisnika.Any(a => a.ProjekatId == p.Id)).ToList();

                context.KorisnikProjekti.RemoveRange(obrisatiKorisnikProjekte);

                context.KorisnikProjekti.AddRange(dodatiKorisnikProjekte.Select(n => new KorisnikProjekat
                {
                    ProjekatId = n.Id,
                    KorisnikUlogaId = korisnikUlogaId
                }));

            }

            // Provjeriti pravo dodavanja uloge
            var dozvoljeneUloge = pravoUpravljanjaKorisnikomService.VratiPravaUpravljanjaKorisnikom(trenutni.TrenutnaUlogaId);
            if (model.Uloge.Any(a => dozvoljeneUloge.All(doz => doz.UlogaUpravljanogId != a.VrstaUloge.Id)))
            {
                return Error("Nemate prava izmjene korisnika sa tim ulogama");
            }

            var stareUloge = context.KorisnikUloge
                                    .Where(a => a.KorisnickoIme == korisnickoIme).ToList();

            // Koristi se transakcija zato sto se brisu uloge pa se onda ponovo dodaju
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Obirisi uloge i dodatne informacije
                    /*foreach (var uloga in stareUloge)
                    {
                        context.KorisnikUloge.Remove(uloga);
                    }
                    context.KorisnikUloge.RemoveRange(stareUloge);

                    context.SaveChanges();

                    foreach (var novaUloga in model.Uloge)
                    {
                        var nova = new KorisnikUloga
                        {
                            UlogaId = novaUloga.VrstaUloge.Id,
                            KorisnickoIme = korisnickoIme
                            // KorisnikUlogaDodatnaInformacija = new List<KorisnikUlogaDodatnaInformacija>()
                        };

                        context.KorisnikUloge.Add(nova);
                    }*/

                    // postavi vrijednosti
                    korisnik.Email = model.Email;
                    korisnik.PunoIme = model.PunoIme;


                    SaveChanges(context);

                    if (!string.IsNullOrEmpty(model.Lozinka) && model.Lozinka.Length >= 6)
                        PostaviLozinku(korisnickoIme, model.Lozinka);

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return Error("Greška", false);
                }
            }

            // vrati azuriranog korisnika
            return VratiKorisnikaPoKorisnickomImenu(korisnickoIme);

        }

        public ServiceResult<Nothing> PostaviKorisnikOnemogucen(String korisnickoIme, bool onemogucen)
        {
            // dobavi korisnika ako postoji
            var korisnik = context.Korisnici
                .SingleOrDefault(k => k.KorisnickoIme == korisnickoIme);

            if (korisnik == null)
                return NotFound();

            // postavi vrijednost onemogucen
            korisnik.Onemogucen = onemogucen;

            SaveChanges(context);

            // sve je ok
            return Ok();
        }

        /* public ServiceResult<Nothing> DodajKorisnikaNaProjekat(String korisnickoIme, AzurirajProjekteKorisnikaRequestModel model)
         {
             // dobavi korisnika ako postoji
             //var korisnik = FilterPoUlogama()
             // .SingleOrDefault(k => k.KorisnickoIme == korisnickoIme);

             var korisnikUlogaId = context.KorisnikUloge.Where(a => a.KorisnickoIme == korisnickoIme && a.UlogaId == model.UlogaId).Select(a => a.KorisnikUlogaId).FirstOrDefault();

             /* if (korisnik == null)
                  return NotFound();

             var kp = context.KorisnikProjekti.Where(k => k.KorisnikUlogaId == korisnikUlogaId).AsQueryable();

             Projekat p = new Projekat();


             foreach (var m in model.Projekti)
             {
                 KorisnikProjekat kpr = new KorisnikProjekat();
                 kpr.KorisnikUlogaId = korisnikUlogaId;
                 kpr.ProjekatId = m;
                 context.Add(kpr);
             }


             SaveChanges(context);

             // sve je ok
             return Ok();
         }*/

        List<Entities.Models.Korisnik.Korisnik> FilterPoUlogama()
        {
            var korisnici = new List<Entities.Models.Korisnik.Korisnik>();

            return korisnici.ToList();
        }

        public ServiceResult<Nothing> PromijeniLozinku(String korisnickoIme, String staraLozinka, String novaLozinka)
        {
            // provjeri da li je lozinka ok
            var lozinkaOk = ProvjeriLozinku(korisnickoIme, staraLozinka);
            if (!lozinkaOk.IsOk)
                return lozinkaOk;

            // uradi promjenu lozinke
            return PostaviLozinku(korisnickoIme, novaLozinka);
        }

        public ServiceResult<Nothing> PostaviLozinku(String korisnickoIme, String novaLozinka)
        {
            // dobavi korisnika ako postoji
            var korisnik = context.Korisnici
                .SingleOrDefault(k => k.KorisnickoIme == korisnickoIme);
            if (korisnik == null)
                return NotFound();

            // dobavi hash provider koji radi hashiranje lozinki
            var hashProvider = Scope.Resolve<IHashProvider>();
            if (hashProvider == null)
                return Error("No hash provider registered");

            // izracunaj novu tajnu
            var novaTajna = hashProvider.HashPassword(novaLozinka);
            korisnik.Tajna = novaTajna;

            SaveChanges(context);

            // sve je ok
            return Ok();
        }

        public ServiceResult<Nothing> ProvjeriLozinku(String korisnickoIme, String lozinka)
        {
            // dobavi korisnika ako postoji
            var korisnik = context.Korisnici
                .SingleOrDefault(k => k.KorisnickoIme == korisnickoIme);
            if (korisnik == null)
                return NotFound();

            // dobavi hash provider koji radi hashiranje lozinki
            var hashProvider = Scope.Resolve<IHashProvider>();
            if (hashProvider == null)
                return Error("No hash provider registered");

            // uradi validaciju lozinke
            var valid = hashProvider.ValidatePassword(lozinka, korisnik.Tajna);
            if (!valid)
                return ValidationError("Pogresna lozinka");

            // sve je ok
            return Ok();
        }

        public ServiceResult<KorisnikListModel> VratiSve(ListaKorisnikaRequestModel model)
        {
            var securityLevel = new SecurityLevel();
            var trenutni = authService.TrenutniKorisnik();

            if (trenutni.TrenutnaUloga.VrijednostUAplikaciji != (int)Uloga.Administrator && trenutni.TrenutnaUloga.VrijednostUAplikaciji != (int)Uloga.Moderator)
                return Error("Nemate pravo pregleda liste korisnika.");


            var query = context.Korisnici
                               .Include(a => a.KorisnikUloge)
                                    .ThenInclude(a => a.Uloga)
                               .AsQueryable();

            query = Secure(query, securityLevel);

            if(trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Moderator)
            {
                var korisnikUlogaId = context.KorisnikUloge.Where(a => a.KorisnickoIme == trenutni.KorisnickoIme && a.UlogaId == trenutni.TrenutnaUlogaId).Select(a => a.KorisnikUlogaId).FirstOrDefault();

                var projekti = context.KorisnikProjekti.Where(a => a.KorisnikUlogaId == korisnikUlogaId).Select(a => a.ProjekatId).ToList();

                query = context.KorisnikProjekti.Where(a => projekti.Contains(a.ProjekatId)).Select(k=>k.KorisnikUloga.Korisnik).Include(a => a.KorisnikUloge)
                                    .ThenInclude(a => a.Uloga).Distinct()
                               .AsQueryable();
            }

            if (model.ProjekatId.HasValue)
            {
                query = context.KorisnikProjekti.Where(p => p.ProjekatId == model.ProjekatId).Select(k => k.KorisnikUloga.Korisnik).AsQueryable();

                //query = query.Where(s => s.KorisnikUloge.Equals(model.ProjekatId));
            }

            // uradi filtriranje po nazivu
            if (!String.IsNullOrWhiteSpace(model.Filter))
            {
                var lowerFilter = model.Filter.ToLower();
                query = query.Where(s => s.PunoIme.ToLower().Contains(lowerFilter));
            }
            // uradi filtriranje po korisnickom imenu
            if (!String.IsNullOrWhiteSpace(model.Username))
            {
                var lowerFilter = model.Username.ToLower();
                query = query.Where(s => s.KorisnickoIme.ToLower().Contains(lowerFilter));
            }
            // uradi filtriranje po ulozi
            if (model.UlogaId.HasValue)
            {
                query = query.Where(s => s.KorisnikUloge.Select(a => a.UlogaId).Where(x => x.Equals(model.UlogaId)).Count() > 0);
            }


            // uradi stranicenje
            var total = query.Count();
            var korisnici = query
                .OrderBy(s => s.PunoIme)
                .Skip(model.Page * model.Count - model.Count)
                .Take(model.Count)
                .ToKorisnikListModelItem()
                .ToList();

            var result = new KorisnikListModel
            {
                Items = korisnici,
                Page = model.Page,
                Total = total
            };

            return Ok(result);
        }


        List<Entities.Models.Korisnik.Korisnik> FilterPoUlogama(IQueryable<Entities.Models.Korisnik.Korisnik> korisnici)
        {
            var korisnik = authService.TrenutniKorisnik();

            if (korisnik == null)
                return korisnici.ToList();



            return korisnici.ToList();
        }

        public ServiceResult<KorisnikModel> Kreiraj(KreirajKorisnikaRequestModel model)
        {
            model.KorisnickoIme = model.KorisnickoIme.Trim().ToLower();

            if (model.Uloge == null || !model.Uloge.Any()) //null ili prazna lista
            {
                return Error("Ne može se dodati korisnik bez definisane uloge.");

            }

            //Provjeri da li je korisničko ime zauzeto
            if (context.Korisnici.Include(k => k.KorisnikUloge).FirstOrDefault(x => x.KorisnickoIme == model.KorisnickoIme) != null)
                return Error("Korisničko ime zauzeto.");

            var trenutni = authService.TrenutniKorisnik();

            // Provjeriti pravo dodavanja uloge
            var dozvoljeneUloge = pravoUpravljanjaKorisnikomService.VratiPravaUpravljanjaKorisnikom(trenutni.TrenutnaUlogaId);
            if (model.Uloge.Any(a => dozvoljeneUloge.All(doz => doz.UlogaUpravljanogId != a.VrstaUlogeId)))
            {
                return Error("Nemate prava da dodate korisnika sa tim ulogama");
            }

            var hashProvider = Scope.Resolve<IHashProvider>();
            var tajna = hashProvider.HashPassword(model.Lozinka);

            Entities.Models.Korisnik.Korisnik korisnik = new Entities.Models.Korisnik.Korisnik
            {
                KorisnickoIme = model.KorisnickoIme,
                Tajna = tajna,
                DatumKreiranja = DateTime.Now,
                Email = model.Email,
                PunoIme = model.PunoIme,
                PolId = model.PolId,
                KorisnikUloge = new List<KorisnikUloga>(),
                Onemogucen = false,
                Jezik = (int)Jezici.bs
            };

            try
            {

                foreach (var uloga in model.Uloge)
                {
                    var korisnikUloga = new KorisnikUloga
                    {
                        UlogaId = uloga.VrstaUlogeId,
                        KorisnikUlogaDodatnaInformacija = new List<KorisnikUlogaDodatnaInformacija>()
                    };

                    korisnik.KorisnikUloge.Add(korisnikUloga);
                }
                context.Add(korisnik);


                //SaveChanges(context);



                var korisnikUloge = korisnik.KorisnikUloge.ToList();

                var korisnikUlogaNajveca = korisnikUloge[0].KorisnikUlogaId;

                //provjera koja vrsta uloge se dodaje
                var vrijednostUAplikaciji = context.Uloge.Where(u => u.Id == model.Uloge[0].VrstaUlogeId).Select(u => u.VrijednostUAplikaciji).FirstOrDefault();

                if (vrijednostUAplikaciji == (int)Uloga.Support)
                {
                    var kategorije = new List<int>();

                    foreach (var kat in model.Kategorije)
                    {
                        kategorije.Add(kat.ZahtjevKategorijaId);
                    }
                    var projekti = context.ZahtjevKategorije.Where(z => kategorije.Contains(z.Id)).Select(z => z.DioProjekta.ProjekatId).Distinct();

                    foreach (var projekat in projekti)
                    {
                        var korisnikProjekat = new KorisnikProjekat
                        {
                            ProjekatId = projekat,
                            KorisnikUlogaId = korisnikUlogaNajveca
                        };

                        context.KorisnikProjekti.Add(korisnikProjekat);
                    }
                    foreach (var kategorija in kategorije)
                    {
                        var zahtjevKategorija = new KorisnikKategorija
                        {
                            KorisnickoIme = model.KorisnickoIme,
                            ZahtjevKategorijaId = kategorija
                        };

                        context.KorisnikKategorije.Add(zahtjevKategorija);
                    }

                }
                else if (vrijednostUAplikaciji != (int)Uloga.Administrator)
                {
                    var korisnikProjekti = context.KorisnikProjekti.ToList();

                    foreach (var projekat in model.Projekti)
                    {
                        var korisnikProjekat = new KorisnikProjekat
                        {
                            ProjekatId = projekat.ProjekatId,
                            KorisnikUlogaId = korisnikUlogaNajveca
                        };

                        context.KorisnikProjekti.Add(korisnikProjekat);

                    }

                }


                SaveChanges(context);

            }
            catch (Exception e)
            {
                return Error("Greška", false);
            }
            return VratiKorisnikaPoKorisnickomImenu(korisnik.KorisnickoIme);
        }



        //public ServiceResult<KorisnikModel> Aktiviraj(string aktivacijskiToken)
        //{
        //	var token = context.AktivacijskiTokeni.FirstOrDefault(x => x.Token == aktivacijskiToken &&
        //															   !x.Iskoristen);
        //	if (token == null)
        //		return NotFound();

        //	if (DateTime.Now > token.DatumIsteka)
        //		return Error("Token istekao.");

        //	var korisnik = context.Korisnici.FirstOrDefault(x => x.KorisnickoIme == token.VlasnikKorisnickoIme);

        //	korisnik.EmailVerifikovan = true;
        //	korisnik.Onemogucen = false;
        //	token.Iskoristen = true;

        //	SaveChanges(context);

        //	var result = Map<Korisnik, KorisnikModel>(korisnik);
        //	return result;
        //}




        //public ServiceResult<Nothing> ResetSifre(ResetSifreRequestModel model)
        //{
        //    var token = context.AktivacijskiTokeni.FirstOrDefault(x => x.Token == model.Token &&
        //                                                               !x.Iskoristen);
        //    if (token == null)
        //        return NotFound();

        //    if (DateTime.Now > token.DatumIsteka)
        //        return Error("Token istekao.");

        //    token.Iskoristen = true;

        //    SaveChanges(context);

        //    var korisnik = context.Korisnici.FirstOrDefault(x => x.KorisnickoIme == token.VlasnikKorisnickoIme);

        //    return PostaviLozinku(korisnik.KorisnickoIme, model.NovaLozinka);
        //}


        public ServiceResult<KorisnikModel> AzurirajLicneDetalje(string korisnickoIme, AzurirajLicneDetaljeRequestModel model)
        {
            var securityLevel = new SecurityLevel();

            var trenutniKorisnik = authService.TrenutniKorisnik();

            if (trenutniKorisnik != null && trenutniKorisnik.TrenutnaUloga.VrijednostUAplikaciji != (int)Uloga.Administrator && trenutniKorisnik.KorisnickoIme != korisnickoIme)
            {
                return Error("Nemate pravo pristupa");
            }

            // dobavi razred ako postoji
            var korisnik = Secure(context.Korisnici.AsQueryable(), securityLevel)
                            .SingleOrDefault(k => k.KorisnickoIme == korisnickoIme);
            if (korisnik == null)
                return NotFound();

            if (!String.IsNullOrEmpty(model.StaraLozinka))
            {
                if (!ImaPravo("korisnik_korisnik_promjena_lozinke"))
                {
                    return Error(PrevediTekst("korisnik_zabranjena_promjena_lozinke", korisnik.Jezik));
                }
                var hashProvider = Scope.Resolve<IHashProvider>();
                if (!hashProvider.ValidatePassword(model.StaraLozinka, korisnik.Tajna))
                {
                    return Error(PrevediTekst("neuspjesna_promjena_lozinke", korisnik.Jezik));
                }
                var novaTajna = hashProvider.HashPassword(model.NovaLozinka);
                korisnik.Tajna = novaTajna;

                if (model.Odjavi)
                {
                    // Onemoguci sve tokene
                    var tokeni = context.Tokeni.Where(a => a.VlasnikKorisnickoIme == korisnik.KorisnickoIme
                                                            && a.DatumIsteka > DateTime.Now).ToList();
                    foreach (var token in tokeni)
                    {
                        token.DatumIsteka = DateTime.Now.AddMinutes(-1);
                    }
                }
            }


            // postavi vrijednosti
            korisnik.Email = model.Email;
            korisnik.PunoIme = model.PunoIme;
            korisnik.PolId = model.PolId;
            korisnik.BrojMobitela = model.Telefon;
            korisnik.Jezik = model.Jezik;

            /*var korisnikUloge = korisnik.KorisnikUloge.ToList();

            var korisnikUlogaNajveca = korisnikUloge[0].KorisnikUlogaId;

            var korisnikProjektiKorisnika = Secure(context.KorisnikProjekti.AsQueryable(), securityLevel)
                            .SingleOrDefault(k => k.KorisnikUlogaId == korisnikUlogaNajveca);*/


            SaveChanges(context);

            // vrati azuriranog korisnika
            return VratiKorisnikaPoKorisnickomImenu(korisnickoIme);
        }

        public ServiceResult<JezikModel> DajJezik()
        {
            var korisnik = authService.TrenutniKorisnik();
            var result = new JezikModel
            {
                Id = korisnik.Jezik
            };
            return Ok(result);
        }

        public ServiceResult<Nothing> AzurirajJezik(AzurirajJezikKorisnikaRequestModel model)
        {
            var korisnik = authService.TrenutniKorisnik();
            korisnik.Jezik = model.Id;

            SaveChanges(context);
            return Ok();
        }

        public ServiceResult<List<KorisnikZahtjevKategorijaModel>> VratiSveSupportKorisnikeZaZahtjevKategoriju(int zahtjevKategorijaId)
        {
           var korisnici= context.KorisnikKategorije.Include(k => k.Korisnik).Where(k => k.ZahtjevKategorijaId == zahtjevKategorijaId).Select(k=>k.Korisnik).ToKorisnikZahtjevKategorijaModel().ToList();
           return Ok(korisnici);

        }
    }
}