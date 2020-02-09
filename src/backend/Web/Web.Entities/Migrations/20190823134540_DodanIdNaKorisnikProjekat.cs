using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DodanIdNaKorisnikProjekat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZahtjevKomentari_Zahtjevi_ZahtjevId",
                table: "ZahtjevKomentari");

            migrationBuilder.AlterColumn<int>(
                name: "ZahtjevId",
                table: "ZahtjevKomentari",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KorisnikUlogaId",
                table: "KorisnikUloge",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_KorisnikUloge_KorisnikUlogaId",
                table: "KorisnikUloge",
                column: "KorisnikUlogaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ZahtjevKomentari_Zahtjevi_ZahtjevId",
                table: "ZahtjevKomentari",
                column: "ZahtjevId",
                principalTable: "Zahtjevi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZahtjevKomentari_Zahtjevi_ZahtjevId",
                table: "ZahtjevKomentari");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_KorisnikUloge_KorisnikUlogaId",
                table: "KorisnikUloge");

            migrationBuilder.DropColumn(
                name: "KorisnikUlogaId",
                table: "KorisnikUloge");

            migrationBuilder.AlterColumn<int>(
                name: "ZahtjevId",
                table: "ZahtjevKomentari",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ZahtjevKomentari_Zahtjevi_ZahtjevId",
                table: "ZahtjevKomentari",
                column: "ZahtjevId",
                principalTable: "Zahtjevi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
