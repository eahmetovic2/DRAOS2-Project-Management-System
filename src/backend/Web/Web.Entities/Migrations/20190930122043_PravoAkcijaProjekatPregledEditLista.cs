using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class PravoAkcijaProjekatPregledEditLista : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            var query = @"
            GO
            INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'projekat_projekat_pregled', N'Pregled projekta', (SELECT Id from PravoObjekti WHERE Sifra='projekat'), N'Pregled projekta', 0)

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'administrator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'projekat_projekat_pregled';

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'moderator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'projekat_projekat_pregled';


            GO
            INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'projekat_projekat_edit', N'Edit projekta', (SELECT Id from PravoObjekti WHERE Sifra='projekat'), N'Edit projekta', 0)

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'administrator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'projekat_projekat_edit';

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'moderator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'projekat_projekat_edit';


            GO
            INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'projekat_projekat_lista', N'Lista projekata pregled', (SELECT Id from PravoObjekti WHERE Sifra='projekat'), N'Lista projekata pregled', 0)

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'administrator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'projekat_projekat_lista';

            GO
            INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'moderator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'projekat_projekat_lista';

";

            migrationBuilder.Sql(query);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
