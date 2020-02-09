using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DodanAtributOznakaZaZahtjevStatusIObrisanaTabelaSifOznakeStatusa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SifOznakeStatusa");

            migrationBuilder.AddColumn<int>(
                name: "Oznaka",
                table: "ZahtjevStatusi",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Oznaka",
                table: "ZahtjevStatusi");

            migrationBuilder.CreateTable(
                name: "SifOznakeStatusa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    Vrijeme = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SifOznakeStatusa", x => x.Id);
                });
        }
    }
}
