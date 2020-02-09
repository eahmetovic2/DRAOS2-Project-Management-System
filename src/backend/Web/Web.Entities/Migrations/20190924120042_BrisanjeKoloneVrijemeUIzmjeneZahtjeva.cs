using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Web.Entities.Migrations
{
    public partial class BrisanjeKoloneVrijemeUIzmjeneZahtjeva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vrijeme",
                table: "IzmjeneZahtjeva");

            

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
               name: "Vrijeme",
               table: "IzmjeneZahtjeva",
               nullable: true);
        }
    }
}
