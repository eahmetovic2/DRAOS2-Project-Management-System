using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class IzmjenaTipaKolonaZaVrijemeUTabeliZahtjevi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "PotrošenoVrijeme",
                table: "Zahtjevi",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EstimiranoVrijeme",
                table: "Zahtjevi",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PotrošenoVrijeme",
                table: "Zahtjevi",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EstimiranoVrijeme",
                table: "Zahtjevi",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldNullable: true);
        }
    }
}
