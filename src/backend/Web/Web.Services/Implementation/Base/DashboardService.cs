using Web.Core.Constants;
using Web.Entities.Models.Korisnik;
using Web.Models.Response.Dashboard;
using Web.Services.Result;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Web.Entities;
using Web.Core.Database;
using System;
using System.Collections.Generic;
using Web.Models.Response.Base.Dashboard;
using Web.Models.Mapping.Mappers.Base.ZahtjevMap;
using Emis.Web.Models.Response;
using Web.Services.Implementation.Base;

namespace Web.Services.Implementation
{
    public class DashboardService : IDashboardService
    {
        private readonly Context _context;
        private readonly ITokenService _tokenService;
        private readonly IAuthService authService;

        public DashboardService(Context context, ITokenService tokenService, IAuthService authService)
        {
            _context = context;
            _tokenService = tokenService;
            this.authService = authService;

        }

        public ServiceResult<HeaderModel> Header()
        {
            var korisnik = authService.TrenutniKorisnik();

            HeaderModel model = null;


            return new OkServiceResult<HeaderModel>(model);
        }

        public ServiceResult<DashboardModel> OsnovnoDashboard()
        {
            DashboardModel model = null;
            var trutniKonrisnik = authService.TrenutniKorisnik();

            return new OkServiceResult<DashboardModel>(model);
        }
        public ServiceResult<DashboardModel> VratiStatistikuZaDashboard()
        {
            var securityLevel = new SecurityLevel();
            var trenutni = authService.TrenutniKorisnik();

            var ulogaId = trenutni.TrenutnaUlogaId;

            var korisnikUlogaId = _context.KorisnikUloge.Where(b => b.KorisnickoIme == trenutni.KorisnickoIme && b.UlogaId == ulogaId).Select(b => b.KorisnikUlogaId).FirstOrDefault();

            var projektiId = _context.KorisnikProjekti.Where(b => b.KorisnikUlogaId == korisnikUlogaId).Select(b => b.ProjekatId).ToList();

            int zahtjeviToDo = 0;
            int zahtjeviUProgresu = 0;
            int zahtjeviDone = 0;
            int projekti = 0;

            var result = new DashboardModel();

            if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Administrator)
            {
                zahtjeviToDo = _context.Zahtjevi.Where(z => z.ZahtjevStatus.Oznaka == (int)OznakeStatusa.ToDo && !z.IsDeleted).Count();

                zahtjeviUProgresu = _context.Zahtjevi.Where(z => z.ZahtjevStatus.Oznaka == (int)OznakeStatusa.InProgress && !z.IsDeleted).Count();

                zahtjeviDone = _context.Zahtjevi.Where(z => z.ZahtjevStatus.Oznaka == (int)OznakeStatusa.Done && !z.IsDeleted).Count();

                projekti = _context.Projekti.Count();

                result = new DashboardModel
                {
                    BrojZahtjevaPotrebnoUraditi = zahtjeviToDo,
                    BrojZahtjevaUProgresu = zahtjeviUProgresu,
                    BrojZavrsenihZahtjeva = zahtjeviDone,
                    BrojProjekata = projekti
                };
            }
            else if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Moderator)
            {
                zahtjeviToDo = _context.Zahtjevi.Where(z => projektiId.Contains(z.ProjekatId) && z.ZahtjevStatus.Oznaka == (int)OznakeStatusa.ToDo && !z.IsDeleted).Count();
                zahtjeviUProgresu = _context.Zahtjevi.Where(z => projektiId.Contains(z.ProjekatId) && z.ZahtjevStatus.Oznaka == (int)OznakeStatusa.InProgress && !z.IsDeleted).Count();
                zahtjeviDone = _context.Zahtjevi.Where(z => projektiId.Contains(z.ProjekatId) && z.ZahtjevStatus.Oznaka == (int)OznakeStatusa.Done && !z.IsDeleted).Count();

                projekti = _context.KorisnikProjekti.Where(b => b.KorisnikUlogaId == korisnikUlogaId).Count();

                result = new DashboardModel
                {
                    BrojZahtjevaPotrebnoUraditi = zahtjeviToDo,
                    BrojZahtjevaUProgresu = zahtjeviUProgresu,
                    BrojZavrsenihZahtjeva = zahtjeviDone,
                    BrojProjekata = projekti
                };
            }
            else if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Support)
            {
                var kategorije = _context.KorisnikKategorije.Where(a => a.KorisnickoIme == trenutni.KorisnickoIme).Select(a => a.ZahtjevKategorijaId).ToList();

                zahtjeviToDo = _context.Zahtjevi.Where(z => kategorije.Contains(z.ZahtjevKategorijaId) && z.ZahtjevStatus.Oznaka == (int)OznakeStatusa.ToDo && !z.IsDeleted).Count();
                zahtjeviUProgresu = _context.Zahtjevi.Where(z => kategorije.Contains(z.ZahtjevKategorijaId) && z.ZahtjevStatus.Oznaka == (int)OznakeStatusa.InProgress && !z.IsDeleted).Count();
                zahtjeviDone = _context.Zahtjevi.Where(z => kategorije.Contains(z.ZahtjevKategorijaId) && z.ZahtjevStatus.Oznaka == (int)OznakeStatusa.Done && !z.IsDeleted).Count();
                projekti = _context.KorisnikProjekti.Where(b => b.KorisnikUlogaId == korisnikUlogaId).Count();

                result = new DashboardModel
                {
                    BrojZahtjevaPotrebnoUraditi = zahtjeviToDo,
                    BrojZahtjevaUProgresu = zahtjeviUProgresu,
                    BrojZavrsenihZahtjeva = zahtjeviDone,
                    BrojProjekata = projekti
                };
            }
            else if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.TaskUser)
            {
                int zahtjeviUkupno = _context.Zahtjevi.Where(z => z.CreatedBy == trenutni.KorisnickoIme && !z.IsDeleted).Count();
                int zahtjeviNisuRijeseni = _context.Zahtjevi.Where(z => z.CreatedBy == trenutni.KorisnickoIme && z.ZahtjevStatus.Oznaka != (int)OznakeStatusa.Done && !z.IsDeleted).Count();
                zahtjeviDone = _context.Zahtjevi.Where(z => z.CreatedBy == trenutni.KorisnickoIme && z.ZahtjevStatus.Oznaka == (int)OznakeStatusa.Done && !z.IsDeleted).Count();
                int komentariKorisnika = _context.ZahtjevKomentari.Where(b => b.CreatedBy == trenutni.KorisnickoIme).Count();

                result = new DashboardModel
                {
                    BrojZahtjevaUkupno = zahtjeviUkupno,
                    BrojZahtjevaKojiNisuRijeseni = zahtjeviNisuRijeseni,
                    BrojZavrsenihZahtjeva = zahtjeviDone,
                    BrojKomentaraKorisnika = komentariKorisnika
                };
            }

            return new OkServiceResult<DashboardModel>(result);
        }



        public ServiceResult<List<ZahtjevListModelItem>> ZadnjiZahtjeviKojiSuRijeseni()
        {
            var securityLevel = new SecurityLevel();
            var trenutni = authService.TrenutniKorisnik();

            List<ZahtjevListModelItem> zadnjiZahtjeviKojiSuRijeseni = new List<ZahtjevListModelItem>();
            if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.TaskUser)
            {
                zadnjiZahtjeviKojiSuRijeseni = _context.Zahtjevi.Where(z => z.CreatedBy == trenutni.KorisnickoIme && z.ZahtjevStatus.Oznaka == (int)OznakeStatusa.Done && !z.IsDeleted).OrderBy(d => d.DatumIzmjene).ToZahtjevListModelItem().Take(5)
           .ToList();
            }

            return new OkServiceResult<List<ZahtjevListModelItem>>(zadnjiZahtjeviKojiSuRijeseni);
        }

        public ServiceResult<List<BrojZahtjevaPoProjektuModel>> BrojZahtjevaPoProjektu()
        {
            var securityLevel = new SecurityLevel();
            var trenutni = authService.TrenutniKorisnik();

            var ulogaId = trenutni.TrenutnaUlogaId;

            var korisnikUlogaId = _context.KorisnikUloge.Where(b => b.KorisnickoIme == trenutni.KorisnickoIme && b.UlogaId == ulogaId).Select(b => b.KorisnikUlogaId).FirstOrDefault();

            var projektiIdNaziv = _context.KorisnikProjekti.Where(b => b.KorisnikUlogaId == korisnikUlogaId).Select(b => new { b.Projekat.Id, b.Projekat.Naziv }).ToList();

            List<BrojZahtjevaPoProjektuModel> brojZahtjevaPoProjektu = new List<BrojZahtjevaPoProjektuModel>();

            if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Administrator)
            {
                projektiIdNaziv = _context.Projekti.Select(p => new { p.Id, p.Naziv }).ToList();

                foreach (var p in projektiIdNaziv)
                {
                    BrojZahtjevaPoProjektuModel b = new BrojZahtjevaPoProjektuModel();
                    b.Naziv = p.Naziv;
                    b.UkupanBrojZahtjeva = _context.Zahtjevi.Where(z => z.ProjekatId == p.Id && !z.IsDeleted).Count();
                    brojZahtjevaPoProjektu.Add(b);
                }
            }
            else if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Moderator)
            {

                foreach (var p in projektiIdNaziv)
                {
                    BrojZahtjevaPoProjektuModel b = new BrojZahtjevaPoProjektuModel();
                    b.Naziv = p.Naziv; 
                    b.UkupanBrojZahtjeva = _context.Zahtjevi.Where(z => z.ProjekatId == p.Id && !z.IsDeleted).Count();
                    brojZahtjevaPoProjektu.Add(b);
                }
            }
            else if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Support)
            {
                foreach (var p in projektiIdNaziv)
                {
                    BrojZahtjevaPoProjektuModel b = new BrojZahtjevaPoProjektuModel();
                    b.Naziv = p.Naziv;
                    b.UkupanBrojZahtjeva = _context.Zahtjevi.Where(z => z.ProjekatId == p.Id && !z.IsDeleted).Count();
                    brojZahtjevaPoProjektu.Add(b);
                }
            }

            return new OkServiceResult<List<BrojZahtjevaPoProjektuModel>>(brojZahtjevaPoProjektu);
        }

        public ServiceResult<List<int>> ZavršeniZahtjeviPoDanimaProšlaSedmica()
        {
            var securityLevel = new SecurityLevel();
            var trenutni = authService.TrenutniKorisnik();

            DateTime dateTime = DateTime.Today;

            var a = StartOfWeek(dateTime);//ponedjeljak tekuce sedmice
            a = a.AddDays(-7);


            var ulogaId = trenutni.TrenutnaUlogaId;

            var korisnikUlogaId = _context.KorisnikUloge.Where(b => b.KorisnickoIme == trenutni.KorisnickoIme && b.UlogaId == ulogaId).Select(b => b.KorisnikUlogaId).FirstOrDefault();

            var projekti = _context.KorisnikProjekti.Where(b => b.KorisnikUlogaId == korisnikUlogaId).Select(b => b.ProjekatId).ToList();

            List<int> zavrseniZahtjeviPoDanimaProšlaSedmica = new List<int>();

            if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Administrator)
            {
                for (int i = 0; i < 7; i++)
                {
                    zavrseniZahtjeviPoDanimaProšlaSedmica.Add(_context.Zahtjevi.Where(z => z.ZahtjevStatus.Oznaka == (int)OznakeStatusa.Done && !z.IsDeleted && z.DatumIzmjene >= a && z.DatumIzmjene < a.AddDays(1)).Count());
                    a = a.AddDays(1);
                }
            }
            else if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Moderator)
            {
                for (int i = 0; i < 7; i++)
                {
                    zavrseniZahtjeviPoDanimaProšlaSedmica.Add(_context.Zahtjevi.Where(z => projekti.Contains(z.ProjekatId) && z.ZahtjevStatus.Oznaka == (int)OznakeStatusa.Done && !z.IsDeleted && z.DatumIzmjene >= a && z.DatumIzmjene < a.AddDays(1)).Count());
                    a = a.AddDays(1);
                }
            }
            else if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Support)
            {
                var kategorije = _context.KorisnikKategorije.Where(k => k.KorisnickoIme == trenutni.KorisnickoIme).Select(k => k.ZahtjevKategorijaId).ToList();

                for (int i = 0; i < 7; i++)
                {
                    zavrseniZahtjeviPoDanimaProšlaSedmica.Add(_context.Zahtjevi.Where(z => kategorije.Contains(z.ZahtjevKategorijaId) && z.ZahtjevStatus.Oznaka == (int)OznakeStatusa.Done && !z.IsDeleted && z.DatumIzmjene >= a && z.DatumIzmjene < a.AddDays(1)).Count());
                    a = a.AddDays(1);
                }
            }


            return new OkServiceResult<List<int>>(zavrseniZahtjeviPoDanimaProšlaSedmica);
        }

        public ServiceResult<List<int>> KreiraniZahtjeviPoDanimaProšlaSedmica()
        {

            var securityLevel = new SecurityLevel();
            var trenutni = authService.TrenutniKorisnik();

            DateTime dateTime = DateTime.Today;
            var a = StartOfWeek(dateTime);//ponedjeljak tekuce sedmice
            a = a.AddDays(-7);

            var ulogaId = trenutni.TrenutnaUlogaId;

            var korisnikUlogaId = _context.KorisnikUloge.Where(b => b.KorisnickoIme == trenutni.KorisnickoIme && b.UlogaId == ulogaId).Select(b => b.KorisnikUlogaId).FirstOrDefault();

            var projekti = _context.KorisnikProjekti.Where(b => b.KorisnikUlogaId == korisnikUlogaId).Select(b => b.ProjekatId).ToList();

            List<int> kreiraniZahtjeviPoDanimaProšlaSedmica = new List<int>();

            if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Administrator)
            {
                for (int i = 0; i < 7; i++)
                {
                    kreiraniZahtjeviPoDanimaProšlaSedmica.Add(_context.Zahtjevi.Where(z =>!z.IsDeleted && z.DatumKreiranja >= a && z.DatumKreiranja < a.AddDays(1)).Count());
                    a = a.AddDays(1);
                }
            }
            else if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Moderator)
            {

                for (int i = 0; i < 7; i++)
                {
                    kreiraniZahtjeviPoDanimaProšlaSedmica.Add(_context.Zahtjevi.Where(z => projekti.Contains(z.ProjekatId) && !z.IsDeleted  && z.DatumKreiranja >= a && z.DatumKreiranja < a.AddDays(1)).Count());
                    a = a.AddDays(1);
                }

            }

            else if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Support)
            {
                var kategorije = _context.KorisnikKategorije.Where(k => k.KorisnickoIme == trenutni.KorisnickoIme).Select(k => k.ZahtjevKategorijaId).ToList();

                for (int i = 0; i < 7; i++)
                {
                    kreiraniZahtjeviPoDanimaProšlaSedmica.Add(_context.Zahtjevi.Where(z => kategorije.Contains(z.ZahtjevKategorijaId) && !z.IsDeleted  && z.DatumKreiranja >= a && z.DatumKreiranja < a.AddDays(1)).Count());
                    a = a.AddDays(1);
                }
            }
            return new OkServiceResult<List<int>>(kreiraniZahtjeviPoDanimaProšlaSedmica);

        }

        public ServiceResult<List<ZahtjevListModelItem>> ZahtjeviNajduzeUPotrebnoUraditi()
        {

            var securityLevel = new SecurityLevel();
            var trenutni = authService.TrenutniKorisnik();

            var ulogaId = trenutni.TrenutnaUlogaId;
            var korisnikUlogaId = _context.KorisnikUloge.Where(b => b.KorisnickoIme == trenutni.KorisnickoIme && b.UlogaId == ulogaId).Select(b => b.KorisnikUlogaId).FirstOrDefault();
            var projekti = _context.KorisnikProjekti.Where(b => b.KorisnikUlogaId == korisnikUlogaId).Select(b => b.ProjekatId).ToList();

            List<ZahtjevListModelItem> zahtjeviNajduzeUPotrebnoUraditi = new List<ZahtjevListModelItem>();
            if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Administrator)
            {
                zahtjeviNajduzeUPotrebnoUraditi = _context.Zahtjevi.Where(z => z.ZahtjevStatus.Oznaka == (int)OznakeStatusa.ToDo && !z.IsDeleted).
                ToZahtjevListModelItem().OrderBy(d => d.DatumKreiranja).Take(5)
               .ToList();
            }
            else if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Moderator)
            {
                zahtjeviNajduzeUPotrebnoUraditi = _context.Zahtjevi.Where(z => projekti.Contains(z.ProjekatId) && z.ZahtjevStatus.Oznaka == (int)OznakeStatusa.ToDo && !z.IsDeleted).
                ToZahtjevListModelItem().OrderBy(d => d.DatumKreiranja).Take(5)
               .ToList();
            }
            else if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Support)
            {
                var kategorije = _context.KorisnikKategorije.Where(a => a.KorisnickoIme == trenutni.KorisnickoIme).Select(a => a.ZahtjevKategorijaId).ToList();

                zahtjeviNajduzeUPotrebnoUraditi = _context.Zahtjevi.Where(z => kategorije.Contains(z.ZahtjevKategorijaId) && z.ZahtjevStatus.Oznaka == (int)OznakeStatusa.ToDo && !z.IsDeleted).
                ToZahtjevListModelItem().OrderBy(d => d.DatumKreiranja).Take(5)
               .ToList();
            }

            return new OkServiceResult<List<ZahtjevListModelItem>>(zahtjeviNajduzeUPotrebnoUraditi);
        }

        public ServiceResult<List<ZahtjevListModelItem>> ZahtjeviZadnjiDodatiKojeJePotrebnoUraditi()
        {
            var securityLevel = new SecurityLevel();
            var trenutni = authService.TrenutniKorisnik();

            var ulogaId = trenutni.TrenutnaUlogaId;
            var korisnikUlogaId = _context.KorisnikUloge.Where(b => b.KorisnickoIme == trenutni.KorisnickoIme && b.UlogaId == ulogaId).Select(b => b.KorisnikUlogaId).FirstOrDefault();
            var projekti = _context.KorisnikProjekti.Where(b => b.KorisnikUlogaId == korisnikUlogaId).Select(b => b.ProjekatId).ToList();

            List<ZahtjevListModelItem> zahtjeviZadnjiDodatiKojeJePotrebnoUraditi = new List<ZahtjevListModelItem>();
            if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Administrator)
            {
                zahtjeviZadnjiDodatiKojeJePotrebnoUraditi = _context.Zahtjevi.Where(z => z.ZahtjevStatus.Oznaka == (int)OznakeStatusa.ToDo && !z.IsDeleted).
                ToZahtjevListModelItem().OrderByDescending(d => d.DatumKreiranja).Take(5)
               .ToList();
            }
            else if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Moderator)
            {
                zahtjeviZadnjiDodatiKojeJePotrebnoUraditi = _context.Zahtjevi.Where(z => projekti.Contains(z.ProjekatId) && z.ZahtjevStatus.Oznaka == (int)OznakeStatusa.ToDo && !z.IsDeleted).
                ToZahtjevListModelItem().OrderByDescending(d => d.DatumKreiranja).Take(5)
               .ToList();
            }
            else if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Support)
            {
                var kategorije = _context.KorisnikKategorije.Where(a => a.KorisnickoIme == trenutni.KorisnickoIme).Select(a => a.ZahtjevKategorijaId).ToList();

                zahtjeviZadnjiDodatiKojeJePotrebnoUraditi = _context.Zahtjevi.Where(z => kategorije.Contains(z.ZahtjevKategorijaId) && z.ZahtjevStatus.Oznaka == (int)OznakeStatusa.ToDo && !z.IsDeleted).
                ToZahtjevListModelItem().OrderByDescending(d => d.DatumKreiranja).Take(5)
               .ToList();
            }

            return new OkServiceResult<List<ZahtjevListModelItem>>(zahtjeviZadnjiDodatiKojeJePotrebnoUraditi);
        }

        public ServiceResult<List<int>> ZahtjeviPoDanimaAktivnaGodina()
        {
            var securityLevel = new SecurityLevel();
            var trenutni = authService.TrenutniKorisnik();

            DateTime dateTime = DateTime.Today;
            var danUGodini = dateTime.DayOfYear;

            var prviDanAktivneGodine = dateTime.AddDays(-danUGodini + 1);


            var ulogaId = trenutni.TrenutnaUlogaId;

            var korisnikUlogaId = _context.KorisnikUloge.Where(b => b.KorisnickoIme == trenutni.KorisnickoIme && b.UlogaId == ulogaId).Select(b => b.KorisnikUlogaId).FirstOrDefault();

            List<int> zavrseniZahtjeviPoDanimaAktivnaGodina = new List<int>() { 0, 0, 0, 0, 0, 0, 0 };
            if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.TaskUser)
            {
                var zahtjevi = _context.Zahtjevi.Where(z => z.CreatedBy == trenutni.KorisnickoIme && z.DatumKreiranja >= prviDanAktivneGodine && !z.IsDeleted).ToZahtjevListModelItem();

                foreach (var i in zahtjevi)
                {
                    DateTime dt = (DateTime)i.DatumKreiranja;


                    switch (dt.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            zavrseniZahtjeviPoDanimaAktivnaGodina[0] += 1;
                            break;
                        case DayOfWeek.Tuesday:
                            zavrseniZahtjeviPoDanimaAktivnaGodina[1] += 1;

                            break;
                        case DayOfWeek.Wednesday:
                            zavrseniZahtjeviPoDanimaAktivnaGodina[2] += 1;
                            break;
                        case DayOfWeek.Thursday:
                            zavrseniZahtjeviPoDanimaAktivnaGodina[3] += 1;
                            break;
                        case DayOfWeek.Friday:
                            zavrseniZahtjeviPoDanimaAktivnaGodina[4] += 1;
                            break;
                        case DayOfWeek.Saturday:
                            zavrseniZahtjeviPoDanimaAktivnaGodina[5] += 1;
                            break;
                        case DayOfWeek.Sunday:
                            zavrseniZahtjeviPoDanimaAktivnaGodina[6] += 1;
                            break;

                    }
                }
            }


            return new OkServiceResult<List<int>>(zavrseniZahtjeviPoDanimaAktivnaGodina);
        }

        public ServiceResult<List<int>> ZahtjeviPoMjesecima()
        {
            var securityLevel = new SecurityLevel();
            var trenutni = authService.TrenutniKorisnik();

            DateTime dateTime = DateTime.Today;
            var danUGodini = dateTime.DayOfYear;

            var prviDanAktivneGodine = dateTime.AddDays(-danUGodini + 1);


            var ulogaId = trenutni.TrenutnaUlogaId;

            var korisnikUlogaId = _context.KorisnikUloge.Where(b => b.KorisnickoIme == trenutni.KorisnickoIme && b.UlogaId == ulogaId).Select(b => b.KorisnikUlogaId).FirstOrDefault();

            List<int> zavrseniZahtjeviPoMjesecima = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.TaskUser)
            {
                var zahtjevi = _context.Zahtjevi.Where(z => z.CreatedBy == trenutni.KorisnickoIme && !z.IsDeleted).ToZahtjevListModelItem();

                foreach (var i in zahtjevi)
                {
                    DateTime dt = (DateTime)i.DatumKreiranja;

                    int mjesec = dt.Month;
                    zavrseniZahtjeviPoMjesecima[mjesec - 1] += 1;

                }
            }


            return new OkServiceResult<List<int>>(zavrseniZahtjeviPoMjesecima);
        }


        public ServiceResult<List<int>> ZahtjeviPoSedmicama()
        {
            var securityLevel = new SecurityLevel();
            var trenutni = authService.TrenutniKorisnik();

            int ukupniBrojSedmica = 5;

            DateTime dateTime = DateTime.Today;
            var a = StartOfWeek(dateTime);

            var pocetakProvjere = dateTime.AddDays(-7* ukupniBrojSedmica); //ponedjeljak Prije Pet Sedmica


            var ulogaId = trenutni.TrenutnaUlogaId;

            var korisnikUlogaId = _context.KorisnikUloge.Where(b => b.KorisnickoIme == trenutni.KorisnickoIme && b.UlogaId == ulogaId).Select(b => b.KorisnikUlogaId).FirstOrDefault();

            List<int> brojZahtjevarRijesenihPoSedmicama = new List<int>();
            if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.TaskUser)
            {

                for(int i=0;i<ukupniBrojSedmica;i++)
                {
                    brojZahtjevarRijesenihPoSedmicama.Add(_context.Zahtjevi.
                        Where(z => z.CreatedBy == trenutni.KorisnickoIme && 
                        z.ZahtjevStatus.Oznaka == (int)OznakeStatusa.Done && !z.IsDeleted &&
                        z.DatumIzmjene >= pocetakProvjere && z.DatumIzmjene<pocetakProvjere.AddDays(7)).Count());
                    pocetakProvjere = pocetakProvjere.AddDays(7);
                }
            }


            return new OkServiceResult<List<int>>(brojZahtjevarRijesenihPoSedmicama);
        }



        public ServiceResult<List<ZahtjevListModelItem>> ZahtjeviZadnjiDodatiNisuRijeseni()
        {
            var securityLevel = new SecurityLevel();
            var trenutni = authService.TrenutniKorisnik();

            var ulogaId = trenutni.TrenutnaUlogaId;
            var korisnikUlogaId = _context.KorisnikUloge.Where(b => b.KorisnickoIme == trenutni.KorisnickoIme && b.UlogaId == ulogaId).Select(b => b.KorisnikUlogaId).FirstOrDefault();
            var projekti = _context.KorisnikProjekti.Where(b => b.KorisnikUlogaId == korisnikUlogaId).Select(b => b.ProjekatId).ToList();

            List<ZahtjevListModelItem> zahtjeviZadnjiDodatiNisuRijeseni = new List<ZahtjevListModelItem>();
            if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.TaskUser)
            {

                zahtjeviZadnjiDodatiNisuRijeseni = _context.Zahtjevi.Where(z => z.CreatedBy == trenutni.KorisnickoIme && z.ZahtjevStatus.Oznaka != (int)OznakeStatusa.Done && !z.IsDeleted).
                ToZahtjevListModelItem().OrderByDescending(d => d.DatumKreiranja).Take(5)
               .ToList();
            }

            return new OkServiceResult<List<ZahtjevListModelItem>>(zahtjeviZadnjiDodatiNisuRijeseni);
        }
        public ServiceResult<List<ZahtjevListModelItem>> ZahtjeviZadnjiDodatiRijeseni()
        {
            var securityLevel = new SecurityLevel();
            var trenutni = authService.TrenutniKorisnik();

            var ulogaId = trenutni.TrenutnaUlogaId;
            var korisnikUlogaId = _context.KorisnikUloge.Where(b => b.KorisnickoIme == trenutni.KorisnickoIme && b.UlogaId == ulogaId).Select(b => b.KorisnikUlogaId).FirstOrDefault();
            var projekti = _context.KorisnikProjekti.Where(b => b.KorisnikUlogaId == korisnikUlogaId).Select(b => b.ProjekatId).ToList();

            List<ZahtjevListModelItem> zahtjeviZadnjiDodatiRijeseni = new List<ZahtjevListModelItem>();
            if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.TaskUser)
            {

                zahtjeviZadnjiDodatiRijeseni = _context.Zahtjevi.Where(z => z.CreatedBy == trenutni.KorisnickoIme && z.ZahtjevStatus.Oznaka == (int)OznakeStatusa.Done && !z.IsDeleted)
                .OrderByDescending(d => d.DatumIzmjene).ToZahtjevListModelItem().Take(5)
               .ToList();
            }

            return new OkServiceResult<List<ZahtjevListModelItem>>(zahtjeviZadnjiDodatiRijeseni);
        }



        public DateTime StartOfWeek(DateTime dt)
        {
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Tuesday:
                    dt = dt.AddDays(-1);
                    break;
                case DayOfWeek.Wednesday:
                    dt = dt.AddDays(-2);
                    break;
                case DayOfWeek.Thursday:
                    dt = dt.AddDays(-3);
                    break;
                case DayOfWeek.Friday:
                    dt = dt.AddDays(-4);
                    break;
                case DayOfWeek.Saturday:
                    dt = dt.AddDays(-5);
                    break;
                case DayOfWeek.Sunday:
                    dt = dt.AddDays(-6);
                    break;

            }
            return dt;
        }

    }
}