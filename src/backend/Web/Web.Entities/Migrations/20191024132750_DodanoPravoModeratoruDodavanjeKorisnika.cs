using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DodanoPravoModeratoruDodavanjeKorisnika : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            var query = @"
            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'moderator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'korisnik_pravo_upravljanja_korisnikom_lista';

";

            migrationBuilder.Sql(query);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
