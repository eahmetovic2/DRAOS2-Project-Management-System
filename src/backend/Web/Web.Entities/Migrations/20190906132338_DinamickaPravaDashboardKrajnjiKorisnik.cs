using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DinamickaPravaDashboardKrajnjiKorisnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            var query = @"
            GO
            INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'osnovno_dashboard_broj_zahtjeva_ukupno', N'Broj zahtjeva ukupno', (SELECT Id from PravoObjekti WHERE Sifra='dashboard'), N'Broj zahtjeva ukupno', 0)

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'taskUser')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_broj_zahtjeva_ukupno';

            GO
            INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'osnovno_dashboard_broj_zahtjeva_nisu_rijeseni', N'Broj zahtjeva koji nisu rijeseni', (SELECT Id from PravoObjekti WHERE Sifra='dashboard'), N'Broj zahtjeva koji nisu rijeseni', 0)

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'taskUser')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_broj_zahtjeva_nisu_rijeseni';

            GO
            INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'osnovno_dashboard_broj_komentara_korisnika_ukupno', N'Broj ukupnih komentara korisnika', (SELECT Id from PravoObjekti WHERE Sifra='dashboard'), N'Broj ukupnih komentara korisnika', 0)

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'taskUser')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_broj_komentara_korisnika_ukupno';

            GO
            INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'osnovno_dashboard_broj_zahtjeva_po_mjesecima', N'Broj zahtjeva po mjesecima', (SELECT Id from PravoObjekti WHERE Sifra='dashboard'), N'Broj zahtjeva po mjesecima', 0)

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'taskUser')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_broj_zahtjeva_po_mjesecima';

            GO
            INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'osnovno_dashboard_broj_zahtjeva_po_danima_aktivna_godina', N'Broj zahtjeva po danima za aktivnu godinu', (SELECT Id from PravoObjekti WHERE Sifra='dashboard'), N'Broj zahtjeva po danima za aktivnu godinu', 0)

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'taskUser')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_broj_zahtjeva_po_danima_aktivna_godina';
            
            GO
            INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'osnovno_dashboard_broj_rijesenih_zahtjeva_po_sedmicama', N'Broj rijesenih zahtjeva po sedmicama', (SELECT Id from PravoObjekti WHERE Sifra='dashboard'), N'Broj rijesenih zahtjeva po sedmicama', 0)

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'taskUser')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_broj_rijesenih_zahtjeva_po_sedmicama';

            GO
            INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'osnovno_dashboard_zahtjevi_zadnji_dodati_nisu_rijeseni', N'Lista zadnjih dodatih zahtjeva koji nisu rijeseni', (SELECT Id from PravoObjekti WHERE Sifra='dashboard'), N'Lista zadnjih dodatih zahtjeva koji nisu rijeseni', 0)

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'taskUser')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_zahtjevi_zadnji_dodati_nisu_rijeseni';


            GO
            INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'osnovno_dashboard_zahtjevi_rijeseni', N'Lista zahtjeva koji su rijeseni', (SELECT Id from PravoObjekti WHERE Sifra='dashboard'), N'Lista zahtjeva koji su rijeseni', 0)

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'taskUser')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'osnovno_dashboard_zahtjevi_rijeseni';";


            migrationBuilder.Sql(query);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
