using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class IzmjenaAtributaRadnoVrijemeIPravoPregledaProjektiAdministrator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "RadnoVrijemeOd",
                table: "ProjekatKonfiguracija",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "RadnoVrijemeDo",
                table: "ProjekatKonfiguracija",
                nullable: false,
                oldClrType: typeof(DateTime));

            var query = @"
             INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
            SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'administrator')
            FROM [dbo].[PravoAkcije]
            WHERE Sifra LIKE 'glavni_meni_osnovno_sidebar_projekti'";
            migrationBuilder.Sql(query);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RadnoVrijemeOd",
                table: "ProjekatKonfiguracija",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RadnoVrijemeDo",
                table: "ProjekatKonfiguracija",
                nullable: false,
                oldClrType: typeof(TimeSpan));
        }
    }
}
