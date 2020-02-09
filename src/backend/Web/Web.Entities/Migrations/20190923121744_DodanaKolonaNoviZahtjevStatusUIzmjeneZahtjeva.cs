using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DodanaKolonaNoviZahtjevStatusUIzmjeneZahtjeva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoviZahtjevStatusId",
                table: "IzmjeneZahtjeva",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_IzmjeneZahtjeva_NoviZahtjevStatusId",
                table: "IzmjeneZahtjeva",
                column: "NoviZahtjevStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_IzmjeneZahtjeva_ZahtjevStatusi_NoviZahtjevStatusId",
                table: "IzmjeneZahtjeva",
                column: "NoviZahtjevStatusId",
                principalTable: "ZahtjevStatusi",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IzmjeneZahtjeva_ZahtjevStatusi_NoviZahtjevStatusId",
                table: "IzmjeneZahtjeva");

            migrationBuilder.DropIndex(
                name: "IX_IzmjeneZahtjeva_NoviZahtjevStatusId",
                table: "IzmjeneZahtjeva");

            migrationBuilder.DropColumn(
                name: "NoviZahtjevStatusId",
                table: "IzmjeneZahtjeva");
        }
    }
}
