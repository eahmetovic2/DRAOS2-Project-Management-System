using Easy.MessageHub;
using Web.Entities.Models.Base;
using Web.Entities.Models.Korisnik;
using Web.Entities.Models.Sifarnik;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Entities.Models;
using System.Linq.Expressions;
using Web.Entities.Models.Zahtjev;
using Web.Entities.Models.Projekat;

namespace Web.Entities
{
    /// <summary>
    /// Db kontekst aplikacije
    /// </summary>
    public class Context : DbContext
    {
        #region Korisnik
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Token> Tokeni { get; set; }
        public DbSet<Modul> Moduli { get; set; }
        public DbSet<FrontendModul> FrontendModuli { get; set; }
        public DbSet<PravoGrupa> PravoGrupe { get; set; }
        public DbSet<PravoObjekt> PravoObjekti { get; set; }
        public DbSet<PravoAkcija> PravoAkcije { get; set; }
        public DbSet<PravoAkcijaUloga> PravoAkcijaUloge { get; set; }
        public DbSet<KorisnikTipDodatneInformacije> KorisnikTipoviDodatneInformacije { get; set; }
        public DbSet<KorisnikUlogaDodatnaInformacija> KorisnikUlogaDodatneInformacije { get; set; }
        public DbSet<UlogaTipDodatneInformacije> UlogaTipoviDodatneInformacije { get; set; }
        public DbSet<PravoUpravljanjaKorisnikom> PravaUpravljanjaKorisnicima { get; set; }
        public DbSet<KorisnikUloga> KorisnikUloge { get; set; }
        public DbSet<KorisnikNotifikacija> KorisnikNotifikacije { get; set; }
        public DbSet<KorisnikKategorija> KorisnikKategorije { get; set; }
        public DbSet<KorisnikProjekat> KorisnikProjekti { get; set; }
        public DbSet<PrebacivanjePrava> PrebacivanjaPrava { get; set; }

        #endregion


        #region Administracija
        public DbSet<Postavke> Postavke { get; set; }
        #endregion


        #region Sifarnik
        public DbSet<Pol> Polovi { get; set; }
        public DbSet<Uloga> Uloge { get; set; }
        #endregion

        #region Log
        public DbSet<VrstaLogAkcija> VrsteLogAkcija { get; set; }
        public DbSet<LogKategorija> LogKategorije { get; set; }
        public DbSet<LogLevel> LogLeveli { get; set; }
        public DbSet<LogAkcija> LogAkcije { get; set; }
        public DbSet<LogEntitet> LogEntiteti { get; set; }
        #endregion

        #region Projekat

        public DbSet<Projekat> Projekti { get; set; }
        public DbSet<DioProjekta> DijeloviProjekta { get; set; }

        public DbSet<ProjekatKonfiguracija> ProjekatKonfiguracija { get; set; }

        public DbSet<ZahtjevKategorija> ZahtjevKategorije { get;set; }

        public DbSet<ZahtjevPrioritet> ZahtjevPrioriteti { get; set; }

        public DbSet<ZahtjevStatus> ZahtjevStatusi { get; set; }
        public DbSet<ZahtjevTip> ZahtjevTipovi { get; set; }

        #endregion

        #region Zahtjev
        public DbSet<Zahtjev> Zahtjevi { get; set; }
        public DbSet<Dokument> Dokumenti { get; set; }

        public DbSet<IzmjenaZahtjeva> IzmjeneZahtjeva { get; set; }

        public DbSet<PrilogKomentar> PriloziKomentar { get; set; }

        public DbSet<PrilogZahtjeva> PriloziZahtjeva { get; set; }
        public DbSet<ZahtjevKomentar> ZahtjevKomentari { get; set; }





        #endregion

        public DbSet<Prevod> Prevodi { get; set; }

        public DbSet<Notifikacija> Notifikacije { get; set; }




        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public override int SaveChanges()
        {
            return SaveChanges(null);
        }

        public int SaveChanges(string korisnickoIme)
        {
            var items = new Dictionary<object, object>();

            //Pravimo listu entry koje moramo poslati u log. Te čuvamo njihove state, jer poslije spašavanja biće urađen reset.
            var entries = ChangeTracker.Entries().Where(e => (e.State == EntityState.Added)
            || (e.State == EntityState.Modified)
            || (e.State == EntityState.Deleted))
            .Select(x => new EntityEntryLog
            {
                Entry = x,
                State = x.State
            }
            ).ToList();

            foreach (var entry in ChangeTracker.Entries().Where(e => (e.State == EntityState.Added) || (e.State == EntityState.Modified)))
            {
                if (entry.Entity is EntityDated)
                {
                    var entityBase = entry.Entity as EntityDated;

                    if (entry.State == EntityState.Added)
                    {
                        entityBase.DatumKreiranja = DateTime.Now;
                    }

                    entityBase.DatumIzmjene = DateTime.Now;
                }

                if (entry.Entity is EntityAutoriziran)
                {
                    var entityBase = entry.Entity as EntityAutoriziran;

                    if (entry.State == EntityState.Added)
                    {
                        entityBase.CreatedBy = korisnickoIme;
                    }

                    entityBase.ModifiedBy = korisnickoIme;
                }
            }

            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted))
            {
                if (entry.Entity is BazniModel)
                {

                    var entityBase = entry.Entity as BazniModel;
                    base.Entry(entityBase).Property("IsDeleted").CurrentValue = true;
                    entityBase.DatumIzmjene = DateTime.Now;
                    entityBase.ModifiedBy = korisnickoIme;
                    entry.State = EntityState.Modified;
                }
            }

            var result = base.SaveChanges();

            //State se mijenja nakon spašavanja, tako da ovdje vraćamo orginalne vrijednosti
            var hub = new MessageHub();
            entries.ForEach(x => hub.Publish<EntityEntryLog>(x));

			return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // podesavamo korisnika
            modelBuilder.Entity<Korisnik>()
                .HasDiscriminator<int>("UlogaDisc")
                .HasValue<Korisnik>((int)Core.Constants.Uloga.Neodredeno);       

            // dodajemo indeks na korisnicko ime korisnika
            modelBuilder.Entity<Korisnik>()
                .HasIndex(k => k.KorisnickoIme)
                .IsUnique();

            // dodajemo indeks na diskriminator korisnika
            modelBuilder.Entity<Korisnik>()
                .HasIndex("UlogaDisc");

            modelBuilder.Entity<PravoAkcija>()
               .HasIndex(a => a.Sifra)
               .IsUnique();

            modelBuilder.Entity<PravoGrupa>()
                .HasIndex(a => a.Sifra)
                .IsUnique();

            modelBuilder.Entity<Modul>()
                .HasIndex(a => a.Sifra)
                .IsUnique();

            modelBuilder.Entity<PravoObjekt>()
                .HasIndex(a => a.Sifra)
                .IsUnique();

            modelBuilder.Entity<PravoAkcijaUloga>()
                        .HasKey(c => new { c.PravoAkcijaId, c.UlogaId });

            modelBuilder.Entity<UlogaTipDodatneInformacije>()
                        .HasKey(c => new { c.UlogaId, c.KorisnikTipDodatneInformacijeId });

            modelBuilder.Entity<PravoUpravljanjaKorisnikom>()
                        .HasKey(c => new { c.UlogaUpraviteljaId, c.UlogaUpravljanogId });

            modelBuilder.Entity<KorisnikUloga>()
                        .HasKey(c => c.KorisnikUlogaId);



            // Omogucavanje soft delete na nivou cijelog sistema
            // U svaku podklasu BaznogModela se doda novo polje IsDeleted koje se koristi za brisanje
            modelBuilder.Model.GetEntityTypes()
                       .Where(entityType => entityType.ClrType.IsSubclassOf(typeof(BazniModel))
                                            && !entityType.ClrType.IsSubclassOf(typeof(Korisnik)))
                       .ToList()
                       .ForEach(entityType =>
                       {
                           modelBuilder.Entity(entityType.ClrType).Property<Boolean>("IsDeleted");
                           var parameter = Expression.Parameter(entityType.ClrType, "e");
                           var body = Expression.Equal(
                               Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(bool) }, parameter, Expression.Constant("IsDeleted")),
                           Expression.Constant(false));
                           modelBuilder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(body, parameter));
                       });

            base.OnModelCreating(modelBuilder);
        }
    }
}
