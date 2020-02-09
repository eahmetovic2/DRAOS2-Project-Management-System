using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DodanaPravaPregledaIIzmjeneDodijeljenogKorisnikaZahtjeva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            var query = @"
            

            GO
            INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'zahtjev_zahtjev_edit_dodijeljeni_korisnik', N'Edit dodijeljeni korisnik na zahtjev', (SELECT Id from PravoObjekti WHERE Sifra='zahtjev'), N'Edit dodijeljeni korisnik na zahtjev', 0)

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'administrator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'zahtjev_zahtjev_edit_dodijeljeni_korisnik';

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'moderator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'zahtjev_zahtjev_edit_dodijeljeni_korisnik';

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'support')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'zahtjev_zahtjev_edit_dodijeljeni_korisnik';
";

            migrationBuilder.Sql(query);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
