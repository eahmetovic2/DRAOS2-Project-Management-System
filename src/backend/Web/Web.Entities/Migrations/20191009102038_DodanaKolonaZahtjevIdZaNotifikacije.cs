using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DodanaKolonaZahtjevIdZaNotifikacije : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ZahtjevId",
                table: "Notifikacije",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notifikacije_ZahtjevId",
                table: "Notifikacije",
                column: "ZahtjevId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifikacije_Zahtjevi_ZahtjevId",
                table: "Notifikacije",
                column: "ZahtjevId",
                principalTable: "Zahtjevi",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifikacije_Zahtjevi_ZahtjevId",
                table: "Notifikacije");

            migrationBuilder.DropIndex(
                name: "IX_Notifikacije_ZahtjevId",
                table: "Notifikacije");

            migrationBuilder.DropColumn(
                name: "ZahtjevId",
                table: "Notifikacije");
        }
    }
}
