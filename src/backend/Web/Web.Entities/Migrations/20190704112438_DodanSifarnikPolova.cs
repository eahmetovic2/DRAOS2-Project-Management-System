using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DodanSifarnikPolova : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Polovi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    Sifra = table.Column<string>(maxLength: 100, nullable: false),
                    Naziv = table.Column<string>(maxLength: 1000, nullable: false),
                    VrijednostUAplikaciji = table.Column<int>(nullable: true),
                    Poredak = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polovi", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Polovi");
        }
    }
}
