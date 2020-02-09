using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DinamickaPravaProjekat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            var sqlFile = Path.Combine(baseDirectory, "Migrations/SQLScripts", "DinamickaPravaProjekat.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile));

            migrationBuilder.AddColumn<int>(
                name: "KomentarId",
                table: "Dokumenti",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Projekti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Naziv = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zahtjevi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Naziv = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true),
                    ProjekatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zahtjevi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zahtjevi_Projekti_ProjekatId",
                        column: x => x.ProjekatId,
                        principalTable: "Projekti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Komentari",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Tekst = table.Column<string>(maxLength: 200, nullable: true),
                    ZahtjevId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komentari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Komentari_Zahtjevi_ZahtjevId",
                        column: x => x.ZahtjevId,
                        principalTable: "Zahtjevi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dokumenti_KomentarId",
                table: "Dokumenti",
                column: "KomentarId");

            migrationBuilder.CreateIndex(
                name: "IX_Komentari_ZahtjevId",
                table: "Komentari",
                column: "ZahtjevId");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtjevi_ProjekatId",
                table: "Zahtjevi",
                column: "ProjekatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dokumenti_Komentari_KomentarId",
                table: "Dokumenti",
                column: "KomentarId",
                principalTable: "Komentari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dokumenti_Komentari_KomentarId",
                table: "Dokumenti");

            migrationBuilder.DropTable(
                name: "Komentari");

            migrationBuilder.DropTable(
                name: "Zahtjevi");

            migrationBuilder.DropTable(
                name: "Projekti");

            migrationBuilder.DropIndex(
                name: "IX_Dokumenti_KomentarId",
                table: "Dokumenti");

            migrationBuilder.DropColumn(
                name: "KomentarId",
                table: "Dokumenti");
        }
    }
}
