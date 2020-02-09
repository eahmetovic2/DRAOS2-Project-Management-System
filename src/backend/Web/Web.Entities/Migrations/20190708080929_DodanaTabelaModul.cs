using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DodanaTabelaModul : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FrontendModulId",
                table: "Uloge",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModulId",
                table: "PravoObjekti",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FrontendModuli",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    Naziv = table.Column<string>(nullable: true),
                    Sifra = table.Column<string>(nullable: true),
                    Onemogucen = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrontendModuli", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Moduli",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    Sifra = table.Column<string>(maxLength: 45, nullable: true),
                    Naziv = table.Column<string>(maxLength: 200, nullable: true),
                    Opis = table.Column<string>(maxLength: 200, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moduli", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Uloge_FrontendModulId",
                table: "Uloge",
                column: "FrontendModulId");

            migrationBuilder.CreateIndex(
                name: "IX_PravoObjekti_ModulId",
                table: "PravoObjekti",
                column: "ModulId");

            migrationBuilder.CreateIndex(
                name: "IX_Moduli_Sifra",
                table: "Moduli",
                column: "Sifra",
                unique: true,
                filter: "[Sifra] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PravoObjekti_Moduli_ModulId",
                table: "PravoObjekti",
                column: "ModulId",
                principalTable: "Moduli",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Uloge_FrontendModuli_FrontendModulId",
                table: "Uloge",
                column: "FrontendModulId",
                principalTable: "FrontendModuli",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PravoObjekti_Moduli_ModulId",
                table: "PravoObjekti");

            migrationBuilder.DropForeignKey(
                name: "FK_Uloge_FrontendModuli_FrontendModulId",
                table: "Uloge");

            migrationBuilder.DropTable(
                name: "FrontendModuli");

            migrationBuilder.DropTable(
                name: "Moduli");

            migrationBuilder.DropIndex(
                name: "IX_Uloge_FrontendModulId",
                table: "Uloge");

            migrationBuilder.DropIndex(
                name: "IX_PravoObjekti_ModulId",
                table: "PravoObjekti");

            migrationBuilder.DropColumn(
                name: "FrontendModulId",
                table: "Uloge");

            migrationBuilder.DropColumn(
                name: "ModulId",
                table: "PravoObjekti");
        }
    }
}
