using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DodanoPravoModeratoruSidebarKorisnici : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            var query = @"
            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'moderator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'glavni_meni_osnovno_sidebar_korisnici';

";

            migrationBuilder.Sql(query);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
