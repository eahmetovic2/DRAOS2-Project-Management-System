using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class NoveKoloneUIzmjeneZahtjeva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "IzmjeneZahtjeva",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumIzmjene",
                table: "IzmjeneZahtjeva",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumKreiranja",
                table: "IzmjeneZahtjeva",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "IzmjeneZahtjeva",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "IzmjeneZahtjeva");

            migrationBuilder.DropColumn(
                name: "DatumIzmjene",
                table: "IzmjeneZahtjeva");

            migrationBuilder.DropColumn(
                name: "DatumKreiranja",
                table: "IzmjeneZahtjeva");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "IzmjeneZahtjeva");
        }
    }
}
