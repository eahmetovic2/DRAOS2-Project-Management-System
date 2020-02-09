using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class PravoIzmjeneStatusaZahtjeva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            var query = @"
            GO
            INSERT [dbo].[PravoGrupe] ([Sifra], [Opis], [Naziv], [IsDeleted]) VALUES (N'zahtjev', N'Zahtjev', N'Zahtjev',0)
            GO
            INSERT [dbo].[Moduli] ([Sifra], [Opis], [Naziv], [IsDeleted]) VALUES (N'zahtjev', N'Zahtjev', N'Zahtjev',0)

            GO
            INSERT [dbo].[PravoObjekti] ([Sifra], [Naziv],[PravoGrupaId], [ModulId],[IsDeleted]) VALUES (N'zahtjev', N'Zahtjev', (SELECT Id from PravoGrupe WHERE Sifra='zahtjev'),(SELECT Id from Moduli WHERE Sifra='zahtjev'),0)

            GO
            INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'zahtjev_zahtjev_edit_status', N'Edit statusa zahtjeva', (SELECT Id from PravoObjekti WHERE Sifra='zahtjev'), N'Edit statusa zahtjeva', 0)

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'administrator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'zahtjev_zahtjev_edit_status';

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'moderator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'zahtjev_zahtjev_edit_status';

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'support')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'zahtjev_zahtjev_edit_status';




";

            migrationBuilder.Sql(query);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
