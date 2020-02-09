using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DodaneDodatneInformacije : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KorisnikTipoviDodatneInformacije",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    Sifra = table.Column<string>(maxLength: 45, nullable: true),
                    Opis = table.Column<string>(maxLength: 200, nullable: true),
                    Naziv = table.Column<string>(maxLength: 200, nullable: true),
                    Poredak = table.Column<int>(nullable: false),
                    Onemogucen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnikTipoviDodatneInformacije", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KorisnikUlogaDodatneInformacije",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    KorisnickoIme = table.Column<string>(maxLength: 64, nullable: true),
                    UlogaId = table.Column<int>(nullable: false),
                    KorisnikTipDodatneInformacijeId = table.Column<int>(nullable: false),
                    Vrijednost = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnikUlogaDodatneInformacije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KorisnikUlogaDodatneInformacije_KorisnikTipoviDodatneInformacije_KorisnikTipDodatneInformacijeId",
                        column: x => x.KorisnikTipDodatneInformacijeId,
                        principalTable: "KorisnikTipoviDodatneInformacije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KorisnikUlogaDodatneInformacije_KorisnikUloge_KorisnickoIme_UlogaId",
                        columns: x => new { x.KorisnickoIme, x.UlogaId },
                        principalTable: "KorisnikUloge",
                        principalColumns: new[] { "KorisnickoIme", "UlogaId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UlogaTipoviDodatneInformacije",
                columns: table => new
                {
                    UlogaId = table.Column<int>(nullable: false),
                    KorisnikTipDodatneInformacijeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UlogaTipoviDodatneInformacije", x => new { x.UlogaId, x.KorisnikTipDodatneInformacijeId });
                    table.ForeignKey(
                        name: "FK_UlogaTipoviDodatneInformacije_KorisnikTipoviDodatneInformacije_KorisnikTipDodatneInformacijeId",
                        column: x => x.KorisnikTipDodatneInformacijeId,
                        principalTable: "KorisnikTipoviDodatneInformacije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UlogaTipoviDodatneInformacije_Uloge_UlogaId",
                        column: x => x.UlogaId,
                        principalTable: "Uloge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikUlogaDodatneInformacije_KorisnikTipDodatneInformacijeId",
                table: "KorisnikUlogaDodatneInformacije",
                column: "KorisnikTipDodatneInformacijeId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikUlogaDodatneInformacije_KorisnickoIme_UlogaId",
                table: "KorisnikUlogaDodatneInformacije",
                columns: new[] { "KorisnickoIme", "UlogaId" });

            migrationBuilder.CreateIndex(
                name: "IX_UlogaTipoviDodatneInformacije_KorisnikTipDodatneInformacijeId",
                table: "UlogaTipoviDodatneInformacije",
                column: "KorisnikTipDodatneInformacijeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KorisnikUlogaDodatneInformacije");

            migrationBuilder.DropTable(
                name: "UlogaTipoviDodatneInformacije");

            migrationBuilder.DropTable(
                name: "KorisnikTipoviDodatneInformacije");
        }
    }
}
