using Web.Entities.Models.Korisnik;
using Web.Models.Response.Korisnik;
using System.Collections.Generic;
using System.Linq;
using Web.Models.Response.Korisnik.PravoAkcija;
using Web.Models.Response.Korisnik.Korisnik;
using Web.Models.Response.Korisnik.PravoUpravljanjaKorisnikom;
using Web.Models.Response.Korisnik.KorisnikDodatneInformacije;
using Web.Models.Response.Korisnik.KorisnikTipDodatneInformacije;
using Web.Entities.Models.Projekat;
using Web.Models.Response.Projekat.ZahtjevKategorija;

namespace Web.Models.Mapping.Mappers.KorisnikMap
{
    public static class KorisnikMapper
    {
        public static IQueryable<KorisnikModel> ToKorisnikModel(this IQueryable<Entities.Models.Korisnik.Korisnik> query)
        {
            return query.Select(korisnik => new KorisnikModel()
            {
                KorisnickoIme = korisnik.KorisnickoIme,
                Email = korisnik.Email,
                PunoIme = korisnik.PunoIme,
                DatumKreiranja = korisnik.DatumKreiranja,
                PolId = korisnik.PolId,
                Onemogucen = korisnik.Onemogucen,
                Jezik = korisnik.Jezik,
                Uloge = korisnik.KorisnikUloge.Select(a => new KorisnikUlogaModel
                {
                    VrstaUloge = new PravoUpravljanjaKorisnikomListModelItem
                    {
                        Id = a.UlogaId,
                        Uloga = a.Uloga.Naziv,
                        PotrebneDodatneInformacije = a.Uloga
                                                      .TipoviDodatneInformacije
                                                      .Select(u => u.KorisnikTipDodatneInformacije.Sifra)
                                                      .ToList()
                    }
                }).ToList(),

                //vraca projekte samo za prvu nadjenu ulogu
                Projekti = korisnik.KorisnikUloge
                .Select(
                    k => k.KorisnikProjekti.Select(
                    p => new KorisnikProjekatModel
                    {
                        Id = p.Projekat.Id,
                        Naziv = p.Projekat.Naziv


                    }).ToList()).FirstOrDefault()
            });
        }

        public static KorisnikModel ToKorisnikModel(this Entities.Models.Korisnik.Korisnik korisnik)
        {
            return new KorisnikModel()
            {
                KorisnickoIme = korisnik.KorisnickoIme,
                Email = korisnik.Email,
                PunoIme = korisnik.PunoIme,
                DatumKreiranja = korisnik.DatumKreiranja,
                PolId = korisnik.PolId,
                Onemogucen = korisnik.Onemogucen,
                Jezik = korisnik.Jezik,
                Uloge = korisnik.KorisnikUloge.Select(a => new KorisnikUlogaModel
                {
                    VrstaUloge = new PravoUpravljanjaKorisnikomListModelItem
                    {
                        Id = a.UlogaId,
                        Uloga = a.Uloga.Naziv,
                        PotrebneDodatneInformacije = a.Uloga
                                                      .TipoviDodatneInformacije
                                                      .Select(u => u.KorisnikTipDodatneInformacije?.Sifra)
                                                      .ToList()
                    }
                }).ToList(),

                //vraca projekte samo za prvu nadjenu ulogu
                Projekti = korisnik.KorisnikUloge
                .Select(
                    k => k.KorisnikProjekti.Select(
                    p => new KorisnikProjekatModel
                    {
                        Id = p.Projekat.Id,
                        Naziv = p.Projekat.Naziv


                    }).ToList()).FirstOrDefault(),

              /*  Kategorije = korisnik.KorisnikUloge.
                Select(
                    k => k.KorisnikProjekti.Select(
                        p => p.KorisnikKategorije.Select(
                            z => new ZahtjevKategorijaModel
                            {
                                Id = z.ZahtjevKategorijaId,
                                Naziv = z.ZahtjevKategorija.Naziv
                            }).ToList())).FirstOrDefault())*/

            };
        }

        public static IQueryable<KorisnikTokenModel> ToKorisnikTokenModel(this IQueryable<Entities.Models.Korisnik.Korisnik> query)
        {
            return query.Select(korisnik => new KorisnikTokenModel()
            {
                KorisnickoIme = korisnik.KorisnickoIme,
                TrenutnaUlogaId = korisnik.TrenutnaUlogaId,
                Email = korisnik.Email,
                PunoIme = korisnik.PunoIme,
                DatumKreiranja = korisnik.DatumKreiranja,
                Onemogucen = korisnik.Onemogucen,
                DodatneInformacije = korisnik.TrenutnaUloga
                                                .KorisnikUloge
                                                .Where(a => a.KorisnickoIme == korisnik.KorisnickoIme)
                                                .Select(a => new KorisnikDodatneInformacijeListModel
                                                {
                                                    Items = a.KorisnikUlogaDodatnaInformacija.Select(dodatno => new KorisnikDodatneInformacijeListModelItem
                                                    {
                                                        KorisnickoIme = dodatno.KorisnikUloga.KorisnickoIme,
                                                        TipDodatneInformacije = new KorisnikTipDodatneInformacijeModel
                                                        {
                                                            Id = dodatno.KorisnikTipDodatneInformacije.Id,
                                                            Naziv = dodatno.KorisnikTipDodatneInformacije.Naziv,
                                                            Onemogucen = dodatno.KorisnikTipDodatneInformacije.Onemogucen,
                                                            Opis = dodatno.KorisnikTipDodatneInformacije.Opis,
                                                            Poredak = dodatno.KorisnikTipDodatneInformacije.Poredak,
                                                            Sifra = dodatno.KorisnikTipDodatneInformacije.Sifra
                                                        }
                                                    }).ToList()
                                                }).First(),
                DozvoljeneAkcije = new PravoAkcijaListModel
                {
                    Items = korisnik.TrenutnaUloga
                                    .PravoAkcijaUloge
                                    .Select(akcija => new PravoAkcijaListModelItem
                                    {
                                        Id = akcija.PravoAkcija.Id,
                                        Naziv = akcija.PravoAkcija.Naziv,
                                        Opis = akcija.PravoAkcija.Opis,
                                        Sifra = akcija.PravoAkcija.Sifra
                                    }).ToList()
                }
            });
        }

        public static IQueryable<KorisnikListModelItem> ToKorisnikListModelItem(this IQueryable<Entities.Models.Korisnik.Korisnik> query)
        {
            return query.Select(korisnik => new KorisnikListModelItem()
            {
                KorisnickoIme = korisnik.KorisnickoIme,
                Uloga = string.Join(", ", korisnik.KorisnikUloge
                                                  .Select(a => a.Uloga.Naziv)
                                                  .ToList()),
                Email = korisnik.Email,
                PunoIme = korisnik.PunoIme,
                DatumKreiranja = korisnik.DatumKreiranja,
                Onemogucen = korisnik.Onemogucen
            });
        }
    }
}
