using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DinamickaPravaDashboardAdminModeratorSupport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           /* migrationBuilder.DropForeignKey(
                name: "FK_KorisnikProjekti_Korisnici_KorisnikKorisnickoIme",
                table: "KorisnikProjekti");

            migrationBuilder.DropIndex(
                name: "IX_KorisnikProjekti_KorisnikKorisnickoIme",
                table: "KorisnikProjekti");

            migrationBuilder.DropColumn(
                name: "KorisnikKorisnickoIme",
                table: "KorisnikProjekti");*/

            var query = @"
            GO
            INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'osnovno_dashboard_lista_dodijeljenih_projekata', N'Lista dodijeljenih projekata', (SELECT Id from PravoObjekti WHERE Sifra='dashboard'), N'Lista dodijeljenih projekata', 0)

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'support')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_lista_dodijeljenih_projekata';

            GO
            INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'osnovno_dashboard_zahtjevi_najduze_u_potrebno_uraditi', N'Zahtjevi najduže u potrebno uraditi', (SELECT Id from PravoObjekti WHERE Sifra='dashboard'), N'Zahtjevi najduže u potrebno uraditi', 0)


            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'administrator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_zahtjevi_najduze_u_potrebno_uraditi';


            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'moderator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_zahtjevi_najduze_u_potrebno_uraditi';

            
            GO
            INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'osnovno_dashboard_zadnji_dodati_zahtjevi_potrebno_uraditi', N'Zahtjevi zadnji dodati koje potrebno uraditi', (SELECT Id from PravoObjekti WHERE Sifra='dashboard'), N'Zahtjevi zadnji dodati koje potrebno uraditi', 0)


            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'administrator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_zadnji_dodati_zahtjevi_potrebno_uraditi';

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'moderator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_zadnji_dodati_zahtjevi_potrebno_uraditi';

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'support')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_zadnji_dodati_zahtjevi_potrebno_uraditi';


            GO
            INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'osnovno_dashboard_zavrseni_zahtjevi_prosla_sedmica', N'Završeni zahtjevi prošle sedmice', (SELECT Id from PravoObjekti WHERE Sifra='dashboard'), N'Broj završenih zahtjeva prošle sedmice po danima', 0)


            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'administrator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_zavrseni_zahtjevi_prosla_sedmica';

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'moderator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_zavrseni_zahtjevi_prosla_sedmica';

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'support')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_zavrseni_zahtjevi_prosla_sedmica';


            GO
            INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'osnovno_dashboard_broj_zahtjeva_po_projektu', N'Broj zahtjeva po projektu', (SELECT Id from PravoObjekti WHERE Sifra='dashboard'), N'Broj zahtjeva po projektu', 0)


            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'administrator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_broj_zahtjeva_po_projektu';

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'moderator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_broj_zahtjeva_po_projektu';

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'support')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_broj_zahtjeva_po_projektu';


            GO
            INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'osnovno_dashboard_prijavljeni_zahtjevi_prosle_sedmice', N'Broj prijavljenih zahtjeva prošle sedmice po danima', (SELECT Id from PravoObjekti WHERE Sifra='dashboard'), N'Broj prijavljenih zahtjeva prošle sedmice po danima', 0)


            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'administrator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_prijavljeni_zahtjevi_prosle_sedmice';

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'moderator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_prijavljeni_zahtjevi_prosle_sedmice';

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'support')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_prijavljeni_zahtjevi_prosle_sedmice';


            GO
            INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'osnovno_dashboard_broj_zahtjeva_potrebno_uraditi', N'Ukupan broj zahtjeva koje potrebno uraditi', (SELECT Id from PravoObjekti WHERE Sifra='dashboard'), N'Ukupan broj zahtjeva koje potrebno uraditi', 0)

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'administrator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_broj_zahtjeva_potrebno_uraditi';

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'moderator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_broj_zahtjeva_potrebno_uraditi';

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'support')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_broj_zahtjeva_potrebno_uraditi';


            GO
            INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'osnovno_dashboard_broj_zahtjeva_u_progresu', N'Ukupan broj zahtjeva koji su u progresu', (SELECT Id from PravoObjekti WHERE Sifra='dashboard'), N'Ukupan broj zahtjeva koji su u progresu', 0)

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'administrator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_broj_zahtjeva_u_progresu';

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'moderator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_broj_zahtjeva_u_progresu';

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'support')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_broj_zahtjeva_u_progresu';

            GO
            INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'osnovno_dashboard_broj_projekata', N'Ukupan broj projekata', (SELECT Id from PravoObjekti WHERE Sifra='dashboard'), N'Ukupan broj projekata', 0)

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'administrator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_broj_projekata';

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'moderator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_broj_projekata';

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'support')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_broj_projekata';";


            migrationBuilder.Sql(query);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          /*  migrationBuilder.AddColumn<string>(
                name: "KorisnikKorisnickoIme",
                table: "KorisnikProjekti",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikProjekti_KorisnikKorisnickoIme",
                table: "KorisnikProjekti",
                column: "KorisnikKorisnickoIme");

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnikProjekti_Korisnici_KorisnikKorisnickoIme",
                table: "KorisnikProjekti",
                column: "KorisnikKorisnickoIme",
                principalTable: "Korisnici",
                principalColumn: "KorisnickoIme",
                onDelete: ReferentialAction.Restrict);*/
        }
    }
}
