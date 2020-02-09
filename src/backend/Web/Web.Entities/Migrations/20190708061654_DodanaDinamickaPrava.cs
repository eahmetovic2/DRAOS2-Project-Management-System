using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DodanaDinamickaPrava : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UlogaId",
                table: "Tokeni",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Prevodi",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PolId",
                table: "Korisnici",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "KorisnikUloge",
                columns: table => new
                {
                    KorisnickoIme = table.Column<string>(maxLength: 64, nullable: false),
                    UlogaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnikUloge", x => new { x.KorisnickoIme, x.UlogaId });
                    table.ForeignKey(
                        name: "FK_KorisnikUloge_Korisnici_KorisnickoIme",
                        column: x => x.KorisnickoIme,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnickoIme",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KorisnikUloge_Uloge_UlogaId",
                        column: x => x.UlogaId,
                        principalTable: "Uloge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PravaUpravljanjaKorisnicima",
                columns: table => new
                {
                    UlogaUpraviteljaId = table.Column<int>(nullable: false),
                    UlogaUpravljanogId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PravaUpravljanjaKorisnicima", x => new { x.UlogaUpraviteljaId, x.UlogaUpravljanogId });
                    table.ForeignKey(
                        name: "FK_PravaUpravljanjaKorisnicima_Uloge_UlogaUpraviteljaId",
                        column: x => x.UlogaUpraviteljaId,
                        principalTable: "Uloge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PravaUpravljanjaKorisnicima_Uloge_UlogaUpravljanogId",
                        column: x => x.UlogaUpravljanogId,
                        principalTable: "Uloge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PravoGrupe",
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
                    table.PrimaryKey("PK_PravoGrupe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PravoObjekti",
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
                    PravoGrupaId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PravoObjekti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PravoObjekti_PravoGrupe_PravoGrupaId",
                        column: x => x.PravoGrupaId,
                        principalTable: "PravoGrupe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PravoAkcije",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    Sifra = table.Column<string>(maxLength: 100, nullable: true),
                    Naziv = table.Column<string>(maxLength: 200, nullable: true),
                    Opis = table.Column<string>(maxLength: 200, nullable: true),
                    PravoObjektId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PravoAkcije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PravoAkcije_PravoObjekti_PravoObjektId",
                        column: x => x.PravoObjektId,
                        principalTable: "PravoObjekti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PravoAkcijaUloge",
                columns: table => new
                {
                    PravoAkcijaId = table.Column<int>(nullable: false),
                    UlogaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PravoAkcijaUloge", x => new { x.PravoAkcijaId, x.UlogaId });
                    table.ForeignKey(
                        name: "FK_PravoAkcijaUloge_PravoAkcije_PravoAkcijaId",
                        column: x => x.PravoAkcijaId,
                        principalTable: "PravoAkcije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PravoAkcijaUloge_Uloge_UlogaId",
                        column: x => x.UlogaId,
                        principalTable: "Uloge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tokeni_UlogaId",
                table: "Tokeni",
                column: "UlogaId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_PolId",
                table: "Korisnici",
                column: "PolId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikUloge_UlogaId",
                table: "KorisnikUloge",
                column: "UlogaId");

            migrationBuilder.CreateIndex(
                name: "IX_PravaUpravljanjaKorisnicima_UlogaUpravljanogId",
                table: "PravaUpravljanjaKorisnicima",
                column: "UlogaUpravljanogId");

            migrationBuilder.CreateIndex(
                name: "IX_PravoAkcijaUloge_UlogaId",
                table: "PravoAkcijaUloge",
                column: "UlogaId");

            migrationBuilder.CreateIndex(
                name: "IX_PravoAkcije_PravoObjektId",
                table: "PravoAkcije",
                column: "PravoObjektId");

            migrationBuilder.CreateIndex(
                name: "IX_PravoAkcije_Sifra",
                table: "PravoAkcije",
                column: "Sifra",
                unique: true,
                filter: "[Sifra] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PravoGrupe_Sifra",
                table: "PravoGrupe",
                column: "Sifra",
                unique: true,
                filter: "[Sifra] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PravoObjekti_PravoGrupaId",
                table: "PravoObjekti",
                column: "PravoGrupaId");

            migrationBuilder.CreateIndex(
                name: "IX_PravoObjekti_Sifra",
                table: "PravoObjekti",
                column: "Sifra",
                unique: true,
                filter: "[Sifra] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Korisnici_Polovi_PolId",
                table: "Korisnici",
                column: "PolId",
                principalTable: "Polovi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tokeni_Uloge_UlogaId",
                table: "Tokeni",
                column: "UlogaId",
                principalTable: "Uloge",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Korisnici_Polovi_PolId",
                table: "Korisnici");

            migrationBuilder.DropForeignKey(
                name: "FK_Tokeni_Uloge_UlogaId",
                table: "Tokeni");

            migrationBuilder.DropTable(
                name: "KorisnikUloge");

            migrationBuilder.DropTable(
                name: "PravaUpravljanjaKorisnicima");

            migrationBuilder.DropTable(
                name: "PravoAkcijaUloge");

            migrationBuilder.DropTable(
                name: "PravoAkcije");

            migrationBuilder.DropTable(
                name: "PravoObjekti");

            migrationBuilder.DropTable(
                name: "PravoGrupe");

            migrationBuilder.DropIndex(
                name: "IX_Tokeni_UlogaId",
                table: "Tokeni");

            migrationBuilder.DropIndex(
                name: "IX_Korisnici_PolId",
                table: "Korisnici");

            migrationBuilder.DropColumn(
                name: "UlogaId",
                table: "Tokeni");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Prevodi");

            migrationBuilder.DropColumn(
                name: "PolId",
                table: "Korisnici");
        }
    }
}
