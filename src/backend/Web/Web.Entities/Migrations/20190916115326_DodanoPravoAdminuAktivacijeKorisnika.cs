using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DodanoPravoAdminuAktivacijeKorisnika : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            var query = @"
            GO
            INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'korisnik_korisnik_aktivacija', N'Aktivacija korisnika', (SELECT Id from PravoObjekti WHERE Sifra='korisnik'), N'Aktivacija korisnika', 0)

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'administrator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'korisnik_korisnik_aktivacija';";

            migrationBuilder.Sql(query);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
