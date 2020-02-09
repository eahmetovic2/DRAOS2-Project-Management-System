using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DodanJezikKorisniku : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Jezik",
                table: "Korisnici",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Prevodi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    Tabela = table.Column<string>(nullable: true),
                    RedId = table.Column<int>(nullable: false),
                    Polje = table.Column<string>(nullable: true),
                    Jezik = table.Column<int>(nullable: false),
                    PrevodTekst = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prevodi", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prevodi");

            migrationBuilder.DropColumn(
                name: "Jezik",
                table: "Korisnici");
        }
    }
}
