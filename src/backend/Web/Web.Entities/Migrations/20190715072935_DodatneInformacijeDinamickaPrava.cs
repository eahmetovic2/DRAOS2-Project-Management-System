using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DodatneInformacijeDinamickaPrava : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var query = @"
            GO
            INSERT[dbo].[PravoAkcije]([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES(CAST(N'2019-07-15T11:34:52.8600000' AS DateTime2), CAST(N'2019-07-15T11:34:52.8600000' AS DateTime2), N'base', N'base',  N'korisnik_uloga_lista_tipova_dodatne_informacije', N'List tipova dodatnih informacija', (SELECT Id from PravoObjekti WHERE Sifra='uloga'), N'List tipova dodatnih informacija', 0)
            GO
            INSERT[dbo].[PravoAkcije]([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES(CAST(N'2019-07-15T11:34:52.8600000' AS DateTime2), CAST(N'2019-07-15T11:34:52.8600000' AS DateTime2), N'base', N'base',  N'korisnik_uloga_izmjena_tipova_dodatne_informacije', N'Izmjena tipova dodatnih informacija', (SELECT Id from PravoObjekti WHERE Sifra='uloga'), N'Izmjena tipova dodatnih informacija', 0)
            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'administrator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'korisnik_uloga_lista_tipova_dodatne_informacije';
            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'administrator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'korisnik_uloga_izmjena_tipova_dodatne_informacije'";
            migrationBuilder.Sql(query);
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
