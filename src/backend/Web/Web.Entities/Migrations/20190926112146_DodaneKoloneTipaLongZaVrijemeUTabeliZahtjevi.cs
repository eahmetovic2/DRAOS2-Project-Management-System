using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DodaneKoloneTipaLongZaVrijemeUTabeliZahtjevi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EstimiranoVrijeme",
                table: "Zahtjevi",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PotrošenoVrijeme",
                table: "Zahtjevi",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimiranoVrijeme",
                table: "Zahtjevi");

            migrationBuilder.DropColumn(
                name: "PotrošenoVrijeme",
                table: "Zahtjevi");
        }
    }
}
