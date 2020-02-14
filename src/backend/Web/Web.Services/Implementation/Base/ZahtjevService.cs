using Autofac;
using Emis.Web.Models.Request;
using Emis.Web.Models.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Core.Constants;
using Web.Core.Database;
using Web.Entities;
using Web.Entities.Models.Base;
using Web.Entities.Models.Korisnik;
using Web.Entities.Models.Projekat;
using Web.Entities.Models.Zahtjev;
using Web.Models.Mapping.Mappers.Base.ZahtjevMap;
using Web.Models.Request.Base.Projekat;
using Web.Models.Request.Zahtjev.Zahtjev;
using Web.Models.Response.Dashboard;
using Web.Services.Definition.Base;
using Web.Services.Result;

namespace Web.Services.Implementation.Base
{

    public class ZahtjevService : Service, IZahtjevService
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
        public ZahtjevService(ILifetimeScope scope, Context context, IAuthService authService, IApplicationConfigurationService applicationConfigurationService)
            : base(scope)
        {
            this.context = context;
            this.authService = authService;
            this.applicationConfigurationService = applicationConfigurationService;

        }
        public ServiceResult<ZahtjevListModel> VratiSveZahtjeve(ListaZahtjevaRequestModel model)
        {
            //var securityLevel = new SecurityLevel();
            var trenutni = authService.TrenutniKorisnik();

            //var ulopga = trenutni.TrenutnaUloga.Naziv;
            var query = Enumerable.Empty<Zahtjev>().AsQueryable();
            //var query = new IQueryable<Zahtjev>();


            if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Administrator)
            {

                query = context.Zahtjevi.Where(z => !z.IsDeleted)
                           .AsQueryable();
            }
            else if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Moderator)
            {
                var ulogaId = context.Uloge.Where(a => a.VrijednostUAplikaciji == (int)Uloga.Moderator).Select(a => a.Id).FirstOrDefault();

                var korisnikUlogaId = context.KorisnikUloge.Where(a => a.KorisnickoIme == trenutni.KorisnickoIme && a.UlogaId == ulogaId).Select(a => a.KorisnikUlogaId).FirstOrDefault();

                var projekti = context.KorisnikProjekti.Where(a => a.KorisnikUlogaId == korisnikUlogaId).Select(a => a.ProjekatId).ToList();

                query = context.Zahtjevi.Where(a => projekti.Contains(a.ProjekatId) && !a.IsDeleted).AsQueryable();
            }
            else if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Support)
            {

                var kategorije = context.KorisnikKategorije.Where(a => a.KorisnickoIme == trenutni.KorisnickoIme).Select(a => a.ZahtjevKategorijaId).ToList();

                query = context.Zahtjevi.Where(a => kategorije.Contains(a.ZahtjevKategorijaId) && !a.IsDeleted).AsQueryable();

            }
            else if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.TaskUser)
            {
                query = context.Zahtjevi.Where(a => a.CreatedBy == trenutni.KorisnickoIme && !a.IsDeleted).AsQueryable();

            }


            //query = Secure(query, securityLevel);

            //filtriranje po vremenu
            if (model.PrethodniBrojDana != null)
            {
                //ako je npr. PrethodniBrojDana 1, vraca sve zahtjeve i za jucerasnji dan, a ne npr. u zadnja 24 sata
                DateTime dateTime = DateTime.Today;

                var pocetakProvjere = dateTime.AddDays(-(int)model.PrethodniBrojDana);
                query = query.Where(z => z.DatumKreiranja >= pocetakProvjere);

            }

            if (!model.Done)
            {
                query = query.Where(z => z.ZahtjevStatus.Oznaka != (int)OznakeStatusa.Done);
            }

            if (!model.InProgress)
            {
                query = query.Where(z => z.ZahtjevStatus.Oznaka != (int)OznakeStatusa.InProgress);
            }

            if (!model.ToDo)
            {
                query = query.Where(z => z.ZahtjevStatus.Oznaka != (int)OznakeStatusa.ToDo);
            }

            // uradi filtriranje po projektu
            if (model.ProjekatId != null)
            {
                query = query.Where(s => s.ProjekatId == model.ProjekatId);
            }

            // uradi filtriranje po dijelu projekta
            if (model.DioProjektaId != null)
            {
                query = query.Where(s => s.ZahtjevKategorija.DioProjektaId == model.DioProjektaId);
            }
            // uradi filtriranje po kategoriji zahtjeva
            if (model.KategorijaId != null)
            {
                query = query.Where(s => s.ZahtjevKategorijaId == model.KategorijaId);
            }

            // uradi filtriranje po tipu zahtjeva
            if (model.TipId != null)
            {
                query = query.Where(s => s.ZahtjevTipId == model.TipId);
            }

            // uradi filtriranje po prioritetu zahtjeva
            if (model.PrioritetId != null)
            {
                query = query.Where(s => s.ZahtjevPrioritetId == model.PrioritetId);
            }

            // uradi filtriranje po statusu zahtjeva
            if (model.StatusId != null)
            {
                query = query.Where(s => s.ZahtjevStatusId == model.StatusId);
            }




            if (!String.IsNullOrWhiteSpace(model.Naziv))
            {
                var lowerNaziv = model.Naziv.ToLower();
                query = query.Where(s => s.Naziv.ToLower().Contains(lowerNaziv));
            }
            // uradi filtriranje po opisu zahtjeva
            if (!String.IsNullOrWhiteSpace(model.Opis))
            {
                var lowerOpis = model.Opis.ToLower();
                query = query.Where(s => s.Opis.ToLower().Contains(lowerOpis));
            }

            // uradi filtriranje po nazivu korisnika
            if (!String.IsNullOrWhiteSpace(model.CreatedBy))
            {
                var lowerCreatedBy = model.CreatedBy.ToLower();
                query = query.Where(s => s.CreatedBy.ToLower().Contains(lowerCreatedBy));
            }



            var zahtjevi = query.ToZahtjevListModelItem().OrderByDescending(d => d.DatumKreiranja).OrderBy(a => a.ZahtjevStatus)
               .ToList();

            if (zahtjevi == null)
                return NotFound();

            var total = zahtjevi.Count();

            if (!model.Sve)
            {
                zahtjevi = zahtjevi.Skip(model.Page * model.Count - model.Count)
                        .Take(model.Count).ToList();
            }
            var result = new ZahtjevListModel
            {
                Items = zahtjevi,
                Page = model.Page,
                Total = total
            };

            return Ok(result);
        }

        public ServiceResult<ZahtjevListModel> VratiSveZahtjeveProjekta(int projekatId, ListaZahtjevaProjektaRequestModel model)
        {
            var securityLevel = new SecurityLevel();

            var query = context.Zahtjevi.Where(z => z.ProjekatId == projekatId && !z.IsDeleted)
                               .AsQueryable();

            //query = Secure(query, securityLevel);

            // uradi filtriranje po dijelu projekta
            if (model.DioProjektaId != null)
            {
                query = query.Where(s => s.ZahtjevKategorija.DioProjektaId == model.DioProjektaId);
            }
            // uradi filtriranje po kategoriji zahtjeva
            if (model.KategorijaId != null)
            {
                query = query.Where(s => s.ZahtjevKategorijaId == model.KategorijaId);
            }

            // uradi filtriranje po tipu zahtjeva
            if (model.TipId != null)
            {
                query = query.Where(s => s.ZahtjevTipId == model.TipId);
            }

            // uradi filtriranje po prioritetu zahtjeva
            if (model.PrioritetId != null)
            {
                query = query.Where(s => s.ZahtjevPrioritetId == model.PrioritetId);
            }

            // uradi filtriranje po statusu zahtjeva
            if (model.StatusId != null)
            {
                query = query.Where(s => s.ZahtjevStatusId == model.StatusId);
            }

            if (!String.IsNullOrWhiteSpace(model.Naziv))
            {
                var lowerNaziv = model.Naziv.ToLower();
                query = query.Where(s => s.Naziv.ToLower().Contains(lowerNaziv));
            }
            // uradi filtriranje po opisu zahtjeva
            if (!String.IsNullOrWhiteSpace(model.Opis))
            {
                var lowerOpis = model.Opis.ToLower();
                query = query.Where(s => s.Opis.ToLower().Contains(lowerOpis));
            }

            // uradi filtriranje po nazivu korisnika
            if (!String.IsNullOrWhiteSpace(model.CreatedBy))
            {
                var lowerCreatedBy = model.CreatedBy.ToLower();
                query = query.Where(s => s.CreatedBy.ToLower().Contains(lowerCreatedBy));
            }



            var zahtjeviProjekta = query.ToZahtjevListModelItem().OrderBy(x => x.PocetakIzrade)
                .ToList();

            if (zahtjeviProjekta == null)
                return NotFound();

            var total = zahtjeviProjekta.Count();

            zahtjeviProjekta = zahtjeviProjekta.Skip(model.Page * model.Count - model.Count)
                    .Take(model.Count).ToList();


            var result = new ZahtjevListModel
            {
                Items = zahtjeviProjekta,
                Page = model.Page,
                Total = total
            };

            return Ok(result);
        }





        public ServiceResult<ZahtjevModel> KreirajZahtjevZaProjekat(int projekatId, KreirajZahtjevRequestModel model)
        {


            /*var trenutni = authService.TrenutniKorisnik();

            var hashProvider = Scope.Resolve<IHashProvider>();
            var tajna = hashProvider.HashPassword(model.Lozinka);*/

            if (String.IsNullOrWhiteSpace(model.Naziv))
                return Error("Naziv zahtjeva ne može biti prazan");
            if (model.Naziv.Length > 128)
            {
                return Error("Naziv zahtjeva ne može biti veći od 128");
            }

            if (String.IsNullOrWhiteSpace(model.Opis))
                return Error("Opis zahtjeva ne može biti prazan");

            Zahtjev zahtjev = new Zahtjev();

            zahtjev.Naziv = model.Naziv;
            zahtjev.Opis = model.Opis;
            zahtjev.DatumKreiranja = DateTime.Now;
            zahtjev.ZahtjevKategorijaId = model.ZahtjevKategorijaId;
            zahtjev.ZahtjevPrioritetId = model.ZahtjevPrioritetId;
            zahtjev.ZahtjevTipId = model.ZahtjevTipId;
            zahtjev.PocetakIzrade = model.PocetakIzrade;

            zahtjev.ZahtjevStatusId = OdrediDefaultniZahtjevStatusProjekta(projekatId).Id;


            Projekat projekat = context.Projekti
            .Include(z => z.Zahtjevi)
            .FirstOrDefault(x => x.Id == projekatId);

            if (projekat == null)
                return NotFound();


            projekat.Zahtjevi.Add(zahtjev);


            IzmjenaZahtjeva izmjenaZahtjeva = new IzmjenaZahtjeva();
            izmjenaZahtjeva.DatumKreiranja = zahtjev.DatumKreiranja;
            izmjenaZahtjeva.DodijeljeniKorisnik = zahtjev.DodijeljeniKorisnik;
            izmjenaZahtjeva.NoviZahtjevStatusId = zahtjev.ZahtjevStatusId;
            izmjenaZahtjeva.Zahtjev = zahtjev;
            context.IzmjeneZahtjeva.Add(izmjenaZahtjeva);

            SaveChanges(context);



            //dodavanje priloga zahtjeva, moguce samo jedan fajl trenutno
            if (model.DokumentId != null)
            {
                PrilogZahtjeva prilogZahtjeva = new PrilogZahtjeva();
                prilogZahtjeva.DokumentId = (int)model.DokumentId;
                prilogZahtjeva.ZahtjevId = zahtjev.Id;
                context.PriloziZahtjeva.Add(prilogZahtjeva);

                SaveChanges(context);
            }



            return Ok(zahtjev.ToZahtjevModel());

        }

        public ServiceResult<List<Zahtjev>> VratiZahtjevePoNazivu(string naziv)
        {

            var zahtjevi = context.Zahtjevi.
            Where(z => z.Naziv == naziv)
            .ToList();

            if (zahtjevi == null)
                return NotFound();

            return Ok(zahtjevi);
        }

        public ServiceResult<ZahtjevModel> VratiZahtjev(int zahtjevId)
        {
            var trenutni = authService.TrenutniKorisnik();
            var ulogaId = trenutni.TrenutnaUlogaId;



            if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Moderator)
            {
                var projekatId = context.Zahtjevi.Where(a => a.Id == zahtjevId).Select(a => a.ProjekatId).FirstOrDefault();

                var korisnikUlogaId = context.KorisnikUloge.Where(a => a.KorisnickoIme == trenutni.KorisnickoIme && a.UlogaId == ulogaId).Select(a => a.KorisnikUlogaId).FirstOrDefault();
                var korisnikProjekti = context.KorisnikProjekti.Where(z => z.ProjekatId == projekatId && z.KorisnikUlogaId == korisnikUlogaId);

                if (korisnikProjekti.Count() == 0)
                {
                    return Error("Nemate pravo pristupa ovom zahtjevu");
                }
            }
            if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.Support)
            {

                var kategorije = context.KorisnikKategorije.Where(a => a.KorisnickoIme == trenutni.KorisnickoIme).Select(a => a.ZahtjevKategorijaId).ToList();

                var z = context.Zahtjevi.Where(a => a.Id == zahtjevId && kategorije.Contains(a.ZahtjevKategorijaId)).AsQueryable();
                if (z.Count() == 0)
                {
                    return Error("Nemate pravo pristupa ovom zahtjevu");
                }
            }
            if (trenutni.TrenutnaUloga.VrijednostUAplikaciji == (int)Uloga.TaskUser)
            {
                var z = context.Zahtjevi.Where(a => a.Id == zahtjevId && !a.IsDeleted).FirstOrDefault();
                if (z == null || z.CreatedBy != trenutni.KorisnickoIme)
                {
                    return Error("Nemate pravo pristupa ovom zahtjevu");
                }
            }

            //query = context.Zahtjevi.Where(a => projekti.Contains(a.ProjekatId)).AsQueryable();

            /*TimeSpan potrosenoVrijeme = TimeSpan.FromSeconds(zahtjev.PotrosenoVrijeme);

            zahtjev.PotrosenoVrijeme =*/

            var zahtjev = context.Zahtjevi.Include(z => z.ZahtjevStatus).
            Where(z => z.Id == zahtjevId && !z.IsDeleted).ToZahtjevModel().FirstOrDefault();

            if (zahtjev == null)
                return Error("Zahtjev nije pronadjen.");


            var priloziZahtjeva = context.PriloziZahtjeva.Include(d => d.Dokument).Where(z => z.ZahtjevId == zahtjevId).Select(d => d.Dokument).ToList();

            zahtjev.Dokumenti = priloziZahtjeva;



            return Ok(zahtjev);
        }

        public ServiceResult<ZahtjevModel> AzurirajZahtjev(int zahtjevId, AzurirajZahtjevRequestModel model)
        {

            var zahtjev = context.Zahtjevi.Include(z => z.ZahtjevStatus)
                            .SingleOrDefault(p => p.Id == zahtjevId && !p.IsDeleted);

            if (zahtjev == null)
                return NotFound();

            if (zahtjev.ZahtjevStatus.Oznaka == (int)OznakeStatusa.Done)
                return Error("Izmjena nije moguća, jer je ovaj zahtjev završen");

            if (String.IsNullOrWhiteSpace(model.Naziv))
                return Error("Naziv zahtjeva ne može biti prazan");

            if (String.IsNullOrWhiteSpace(model.Opis))
                return Error("Opis zahtjeva ne može biti prazan");
            IzmjenaZahtjeva izmjenaZahtjeva = new IzmjenaZahtjeva();

            try
            {

                zahtjev.Naziv = model.Naziv;
                zahtjev.Opis = model.Opis;
                zahtjev.ZahtjevKategorijaId = model.ZahtjevKategorijaId;
                zahtjev.ZahtjevTipId = model.ZahtjevTipId;
                zahtjev.ZahtjevPrioritetId = model.ZahtjevPrioritetId;
                zahtjev.DatumIzmjene = DateTime.Now;
                zahtjev.ModifiedBy = model.ModifiedBy;
                zahtjev.PocetakIzrade = model.PocetakIzrade;

                if (ImaPravo("zahtjev_zahtjev_edit_dodijeljeni_korisnik"))
                {
                    if (zahtjev.DodijeljeniKorisnikIme != model.DodijeljeniKorisnikIme)
                    {
                        zahtjev.DodijeljeniKorisnikIme = model.DodijeljeniKorisnikIme;

                        if (zahtjev.DodijeljeniKorisnikIme != null)
                        {
                            var naziv = zahtjev.Naziv;

                            Notifikacija notifikacija = new Notifikacija();
                            notifikacija.Naslov = "Dodjela zahtjeva";
                            if (zahtjev.Naziv.Length > 35)
                                naziv = zahtjev.Naziv.Substring(0, 32) + "...";
                            notifikacija.Poruka = "Dodijeljen Vam je zahtjev pod nazivom: '" + naziv;
                            notifikacija.ZahtjevId = zahtjev.Id;
                            notifikacija.ProjekatId = zahtjev.ProjekatId;
                            notifikacija.DatumKreiranja = DateTime.Now;
                            context.Notifikacije.Add(notifikacija);



                            //SaveChanges(context);

                            //kreiranje notifikacije korisniku koji je kreirao zahtjev
                            KorisnikNotifikacija korisnikNotifikacija = new KorisnikNotifikacija();
                            korisnikNotifikacija.KorisnickoIme = zahtjev.DodijeljeniKorisnikIme;
                            korisnikNotifikacija.NotifikacijaId = notifikacija.Id;
                            korisnikNotifikacija.Otvorena = false;
                            korisnikNotifikacija.Notifikacija = notifikacija;

                            context.KorisnikNotifikacije.Add(korisnikNotifikacija);
                        }

                    }
                }

                //ako se mijenja i status, da se to zabiljezi u bazi promjena statusa
                if (zahtjev.ZahtjevStatusId != model.ZahtjevStatusId)
                {
                    if (!ImaPravo("zahtjev_zahtjev_edit_status"))
                    {
                        return Error("Nemate pravo izmjene statusa zahtjeva.");
                    }
                    AzurirajStatusZahtjevaRequestModel azurirajStatusZahtjevaRequestModel = new AzurirajStatusZahtjevaRequestModel();
                    azurirajStatusZahtjevaRequestModel.ZahtjevStatusId = model.ZahtjevStatusId;

                    this.AzurirajStatusZahtjeva(zahtjevId, azurirajStatusZahtjevaRequestModel);


                }

                SaveChanges(context);
            }
            catch (Exception e)
            {
                throw;
            }

            return Ok(zahtjev.ToZahtjevModel());
        }

        public ZahtjevStatus OdrediDefaultniZahtjevStatusProjekta(int projekatId)
        {
            var zahtjevStatus = context.ZahtjevStatusi
                          .Where(p => p.ProjekatId == projekatId && p.Default == true).FirstOrDefault();

            return zahtjevStatus;
        }

        public ServiceResult<Nothing> AzurirajStatusZahtjeva(int zahtjevId, AzurirajStatusZahtjevaRequestModel model)
        {
            var trenutni = authService.TrenutniKorisnik();

            if (!ImaPravo("zahtjev_zahtjev_edit_status"))
            {
                return Error("Nemate pravo izmjene statusa zahtjeva.");
            }


            var zahtjev = context.Zahtjevi.Include(z => z.ZahtjevStatus)
                .Include(z => z.Projekat).ThenInclude(z => z.ProjekatKonfiguracija)
                            .SingleOrDefault(p => p.Id == zahtjevId && !p.IsDeleted);

            if (zahtjev == null)
                return NotFound();
            var zahtjevStatus = context.ZahtjevStatusi.SingleOrDefault(z => z.Id == model.ZahtjevStatusId);

            var zadnjaIzmjenaStatusa = context.IzmjeneZahtjeva.Include(z => z.NoviZahtjevStatus).Where(z => z.ZahtjevId == zahtjevId).OrderByDescending(z => z.DatumKreiranja).FirstOrDefault();

            try
            {
                IzmjenaZahtjeva izmjenaZahtjeva = new IzmjenaZahtjeva();

                if (zahtjev.ProjekatId == zahtjevStatus.ProjekatId)
                {
                    int prijasnjaOznakaStatusa = zahtjev.ZahtjevStatus.Oznaka;
                    //zahtjev.ZahtjevStatusId = model.ZahtjevStatusId;
                    zahtjev.ZahtjevStatus = zahtjevStatus;

                    izmjenaZahtjeva.ZahtjevId = zahtjevId;
                    izmjenaZahtjeva.NoviZahtjevStatusId = model.ZahtjevStatusId;
                    izmjenaZahtjeva.DodijeljeniKorisnikIme = zahtjev.DodijeljeniKorisnikIme;
                    izmjenaZahtjeva.DatumKreiranja = DateTime.Now;
                    izmjenaZahtjeva.DatumIzmjene = DateTime.Now;
                    context.IzmjeneZahtjeva.Add(izmjenaZahtjeva);


                    string radniDani = zahtjev.Projekat.ProjekatKonfiguracija.RadniDani;
                    var vrijemeOd = zahtjev.Projekat.ProjekatKonfiguracija.RadnoVrijemeOd;
                    var vrijemeDo = zahtjev.Projekat.ProjekatKonfiguracija.RadnoVrijemeDo;



                    //racunanje vremena statusa ako je prijasnji status bio u progresu
                    if (zadnjaIzmjenaStatusa != null)
                    {

                        if (zadnjaIzmjenaStatusa.NoviZahtjevStatus.Oznaka == (int)OznakeStatusa.InProgress)
                        {
                            /*TimeSpan time = (TimeSpan)(izmjenaZahtjeva.DatumKreiranja - zadnjaIzmjenaStatusa.DatumKreiranja);
                            if (zahtjev.PotrošenoVrijeme != null)
                                zahtjev.PotrošenoVrijeme += (long)time.TotalSeconds;
                            else zahtjev.PotrošenoVrijeme = (long)time.TotalSeconds;*/

                            long totalSeconds = RacunajVrijemeTokomRadnogVremena((DateTime)zadnjaIzmjenaStatusa.DatumKreiranja, (DateTime)izmjenaZahtjeva.DatumKreiranja, radniDani, vrijemeOd, vrijemeDo);

                            if (zahtjev.PotrošenoVrijeme != null)
                                zahtjev.PotrošenoVrijeme += totalSeconds;
                            else zahtjev.PotrošenoVrijeme = totalSeconds;

                        }
                    }
                    else
                    {
                        if (prijasnjaOznakaStatusa == (int)OznakeStatusa.InProgress)
                        {
                            long totalSeconds = RacunajVrijemeTokomRadnogVremena((DateTime)zahtjev.DatumKreiranja, (DateTime)izmjenaZahtjeva.DatumKreiranja, radniDani, vrijemeOd, vrijemeDo);

                            if (zahtjev.PotrošenoVrijeme != null)
                                zahtjev.PotrošenoVrijeme += totalSeconds;
                            else zahtjev.PotrošenoVrijeme = totalSeconds;
                            /*TimeSpan time = (TimeSpan)(izmjenaZahtjeva.DatumKreiranja - zahtjev.DatumKreiranja);
                            if (zahtjev.PotrošenoVrijeme != null)
                                zahtjev.PotrošenoVrijeme += (long)time.TotalSeconds;
                            else zahtjev.PotrošenoVrijeme = (long)time.TotalSeconds;*/
                        }
                    }

                    var naziv = zahtjev.Naziv;


                    //kreiranje notifikacije o promjeni statusa zahtjeva
                    Notifikacija notifikacija = new Notifikacija();
                    notifikacija.Naslov = "Izmjena statusa zahtjeva";
                    if (zahtjev.Naziv.Length > 35)
                        naziv = zahtjev.Naziv.Substring(0, 32) + "...";
                    notifikacija.Poruka = "Status zahtjeva pod nazivom: '" + naziv + "', promijenjen u status: " + zahtjev.ZahtjevStatus.Naziv;
                    notifikacija.ZahtjevId = zahtjev.Id;
                    notifikacija.ProjekatId = zahtjev.ProjekatId;
                    notifikacija.DatumKreiranja = DateTime.Now;
                    context.Notifikacije.Add(notifikacija);


                    //kreiranje notifikacije korisniku koji je kreirao zahtjev
                    KorisnikNotifikacija korisnikNotifikacija = new KorisnikNotifikacija();
                    korisnikNotifikacija.KorisnickoIme = zahtjev.CreatedBy;
                    korisnikNotifikacija.NotifikacijaId = notifikacija.Id;
                    korisnikNotifikacija.Otvorena = false;
                    korisnikNotifikacija.Notifikacija = notifikacija;

                    context.KorisnikNotifikacije.Add(korisnikNotifikacija);

                    SaveChanges(context);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return Ok();
        }

        public ServiceResult<List<IzmjenaZahtjeva>> VratiIzmjeneStatusaZahtjeva(int zahtjevId)
        {
            var historijaZahtjeva = context.IzmjeneZahtjeva.Where(i => i.ZahtjevId == zahtjevId).Include(i => i.NoviZahtjevStatus).OrderBy(i => i.DatumKreiranja).ToList();
            return Ok(historijaZahtjeva);
        }

        public ServiceResult<Nothing> ObrisiZahtjev(int zahtjevId)
        {
            var zahtjev = context.Zahtjevi.Where(z => z.Id == zahtjevId).AsQueryable().First();
            zahtjev.IsDeleted = true;
            SaveChanges(context);
            return Ok();
        }

        public long RacunajVrijemeTokomRadnogVremena(DateTime pocetak, DateTime kraj, string radniDani, TimeSpan vrijemeOd, TimeSpan vrijemeDo)
        {
            long potrosenoVrijeme = 0;
            if (pocetak.Date != kraj.Date)
            {
                var dan = pocetak.Date.AddDays(1);

                bool radniDan = false;

                //racunanje vremena za one dane koji su izmedju pocetnog i krajnjeg dana statusa u kojem je zahtjev bio
                while (dan != kraj.Date)
                {
                    radniDan = false;

                    if (radniDani.ElementAt(((int)dan.DayOfWeek + 6) % 7) == '1')
                        radniDan = true;

                    if (radniDan)
                    {
                        TimeSpan ts = (TimeSpan)(vrijemeDo - vrijemeOd);
                        long ukupnoVrijemeRadnogDanaUSekundama = (long)ts.TotalSeconds;
                        potrosenoVrijeme += ukupnoVrijemeRadnogDanaUSekundama;
                    }

                    dan = dan.AddDays(1);
                }


                DateTime pocetakVremenaZahtjeva = pocetak.Date;
                if (pocetak.TimeOfDay < vrijemeOd)
                    pocetakVremenaZahtjeva = pocetakVremenaZahtjeva.Add(vrijemeOd);
                else pocetakVremenaZahtjeva = pocetakVremenaZahtjeva.Add(pocetak.TimeOfDay);


                DateTime krajVremenaZahtjeva = kraj.Date;
                if (kraj.TimeOfDay > vrijemeDo)
                    krajVremenaZahtjeva = krajVremenaZahtjeva.Add(vrijemeDo);
                else
                    krajVremenaZahtjeva = krajVremenaZahtjeva.Add(kraj.TimeOfDay);


                if (radniDani.ElementAt(((int)pocetak.DayOfWeek + 6) % 7) == '1')
                {
                    TimeSpan time = (TimeSpan)(vrijemeDo - pocetakVremenaZahtjeva.TimeOfDay);
                    if (time.TotalSeconds > 0)
                        potrosenoVrijeme += (long)time.TotalSeconds;
                }

                if (radniDani.ElementAt(((int)kraj.DayOfWeek + 6)%7) == '1')
                {
                    TimeSpan time = (TimeSpan)(krajVremenaZahtjeva.TimeOfDay - vrijemeOd);
                    if (time.TotalSeconds > 0)
                        potrosenoVrijeme += (long)time.TotalSeconds;
                }

            }
            else
            {

                DateTime pocetakVremenaZahtjeva = pocetak.Date;
                if (pocetak.TimeOfDay < vrijemeOd)
                    pocetakVremenaZahtjeva = pocetakVremenaZahtjeva.Add(vrijemeOd);
                else pocetakVremenaZahtjeva = pocetakVremenaZahtjeva.Add(pocetak.TimeOfDay);


                DateTime krajVremenaZahtjeva = kraj.Date;
                if (kraj.TimeOfDay > vrijemeDo)
                    krajVremenaZahtjeva = krajVremenaZahtjeva.Add(vrijemeDo);
                else
                    krajVremenaZahtjeva = krajVremenaZahtjeva.Add(kraj.TimeOfDay);

                if (radniDani.ElementAt(((int)pocetak.DayOfWeek + 6)%7) == '1')
                {

                    TimeSpan time = (TimeSpan)(krajVremenaZahtjeva.TimeOfDay - pocetakVremenaZahtjeva.TimeOfDay);
                    if (time.TotalSeconds > 0)
                        potrosenoVrijeme += (long)time.TotalSeconds;
                }

            }
            return potrosenoVrijeme;
        }




        /*   public ServiceResult<Nothing> ObrisiProjekat(int id)
   {
      var projekat = context.Projekti.AsQueryable().
          SingleOrDefault(p => p.Id == id);
      if (projekat == null)
          return NotFound();
      context.Remove(projekat);
      SaveChanges(context);
      return Ok();
   }*/
    }
}

