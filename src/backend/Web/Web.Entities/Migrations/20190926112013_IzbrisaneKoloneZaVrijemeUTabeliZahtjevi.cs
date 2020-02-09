using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class IzbrisaneKoloneZaVrijemeUTabeliZahtjevi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimiranoVrijeme",
                table: "Zahtjevi");

            migrationBuilder.DropColumn(
                name: "PotrošenoVrijeme",
                table: "Zahtjevi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "EstimiranoVrijeme",
                table: "Zahtjevi",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "PotrošenoVrijeme",
                table: "Zahtjevi",
                nullable: true);
        }
    }
}
