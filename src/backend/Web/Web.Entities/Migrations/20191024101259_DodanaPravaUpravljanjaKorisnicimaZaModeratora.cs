using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DodanaPravaUpravljanjaKorisnicimaZaModeratora : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            var query = @"
            GO
            INSERT INTO [dbo].[PravaUpravljanjaKorisnicima] ([UlogaUpraviteljaId], [UlogaUpravljanogId] )
            VALUES ((SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'moderator'),(SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'support'));
            
            GO
            INSERT INTO [dbo].[PravaUpravljanjaKorisnicima] ([UlogaUpraviteljaId], [UlogaUpravljanogId] )
            VALUES ((SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'moderator'),(SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'taskUser'));
";

            migrationBuilder.Sql(query);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
