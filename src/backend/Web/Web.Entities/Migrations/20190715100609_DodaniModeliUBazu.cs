using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DodaniModeliUBazu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dokumenti_Komentari_KomentarId",
                table: "Dokumenti");

            migrationBuilder.DropTable(
                name: "Komentari");

            migrationBuilder.DropIndex(
                name: "IX_Dokumenti_KomentarId",
                table: "Dokumenti");

            migrationBuilder.DropColumn(
                name: "KomentarId",
                table: "Dokumenti");

            migrationBuilder.AddColumn<string>(
                name: "DodijeljeniKorisnikIme",
                table: "Zahtjevi",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimiranoVrijeme",
                table: "Zahtjevi",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PotrošenoVrijeme",
                table: "Zahtjevi",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ZahtjevKategorijaId",
                table: "Zahtjevi",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ZahtjevPrioritetId",
                table: "Zahtjevi",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ZahtjevStatusId",
                table: "Zahtjevi",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ZahtjevTipId",
                table: "Zahtjevi",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjekatKonfiguracijaId",
                table: "Projekti",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DijeloviProjekta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    ProjekatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DijeloviProjekta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DijeloviProjekta_Projekti_ProjekatId",
                        column: x => x.ProjekatId,
                        principalTable: "Projekti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IzmjeneZahtjeva",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Vrijeme = table.Column<DateTime>(nullable: false),
                    KorisnickoIme = table.Column<string>(nullable: true),
                    ZahtjevId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IzmjeneZahtjeva", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IzmjeneZahtjeva_Korisnici_KorisnickoIme",
                        column: x => x.KorisnickoIme,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnickoIme",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IzmjeneZahtjeva_Zahtjevi_ZahtjevId",
                        column: x => x.ZahtjevId,
                        principalTable: "Zahtjevi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KorisnikProjekti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KorisnickoIme = table.Column<string>(nullable: true),
                    ProjekatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnikProjekti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KorisnikProjekti_Korisnici_KorisnickoIme",
                        column: x => x.KorisnickoIme,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnickoIme",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KorisnikProjekti_Projekti_ProjekatId",
                        column: x => x.ProjekatId,
                        principalTable: "Projekti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifikacije",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naslov = table.Column<string>(nullable: true),
                    Poruka = table.Column<string>(nullable: true),
                    ProjekatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifikacije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifikacije_Projekti_ProjekatId",
                        column: x => x.ProjekatId,
                        principalTable: "Projekti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrebacivanjaPrava",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OdKorisnickogImena = table.Column<string>(nullable: true),
                    DoKorisnickogImena = table.Column<string>(nullable: true),
                    VrijemeOd = table.Column<DateTime>(nullable: false),
                    VrijemeDo = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrebacivanjaPrava", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrebacivanjaPrava_Korisnici_DoKorisnickogImena",
                        column: x => x.DoKorisnickogImena,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnickoIme",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrebacivanjaPrava_Korisnici_OdKorisnickogImena",
                        column: x => x.OdKorisnickogImena,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnickoIme",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PriloziZahtjeva",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ZahtjevId = table.Column<int>(nullable: false),
                    DokumentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriloziZahtjeva", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriloziZahtjeva_Dokumenti_DokumentId",
                        column: x => x.DokumentId,
                        principalTable: "Dokumenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriloziZahtjeva_Zahtjevi_ZahtjevId",
                        column: x => x.ZahtjevId,
                        principalTable: "Zahtjevi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjekatKonfiguracija",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RadnoVrijemeOd = table.Column<DateTime>(nullable: false),
                    RadnoVrijemeDo = table.Column<DateTime>(nullable: false),
                    RadniDani = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjekatKonfiguracija", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "ZahtjevKomentari",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Komentar = table.Column<string>(nullable: true),
                    KorisnikId = table.Column<int>(nullable: false),
                    KorisnikKorisnickoIme = table.Column<string>(nullable: true),
                    ZahtjevId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZahtjevKomentari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZahtjevKomentari_Korisnici_KorisnikKorisnickoIme",
                        column: x => x.KorisnikKorisnickoIme,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnickoIme",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZahtjevKomentari_Zahtjevi_ZahtjevId",
                        column: x => x.ZahtjevId,
                        principalTable: "Zahtjevi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZahtjevPrioriteti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    Poredak = table.Column<int>(nullable: false),
                    Default = table.Column<bool>(nullable: false),
                    ProjekatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZahtjevPrioriteti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZahtjevPrioriteti_Projekti_ProjekatId",
                        column: x => x.ProjekatId,
                        principalTable: "Projekti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZahtjevStatusi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    Default = table.Column<bool>(nullable: false),
                    ProjekatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZahtjevStatusi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZahtjevStatusi_Projekti_ProjekatId",
                        column: x => x.ProjekatId,
                        principalTable: "Projekti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZahtjevTipovi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    Default = table.Column<bool>(nullable: false),
                    ProjekatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZahtjevTipovi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZahtjevTipovi_Projekti_ProjekatId",
                        column: x => x.ProjekatId,
                        principalTable: "Projekti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZahtjevKategorije",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    DioProjektaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZahtjevKategorije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZahtjevKategorije_DijeloviProjekta_DioProjektaId",
                        column: x => x.DioProjektaId,
                        principalTable: "DijeloviProjekta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KorisnikNotifikacije",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Otvorena = table.Column<bool>(nullable: false),
                    KorisnickoIme = table.Column<string>(nullable: true),
                    NotifikacijaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnikNotifikacije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KorisnikNotifikacije_Korisnici_KorisnickoIme",
                        column: x => x.KorisnickoIme,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnickoIme",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KorisnikNotifikacije_Notifikacije_NotifikacijaId",
                        column: x => x.NotifikacijaId,
                        principalTable: "Notifikacije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriloziKomentar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ZahtjevKomentarId = table.Column<int>(nullable: false),
                    DokumentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriloziKomentar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriloziKomentar_Dokumenti_DokumentId",
                        column: x => x.DokumentId,
                        principalTable: "Dokumenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriloziKomentar_ZahtjevKomentari_ZahtjevKomentarId",
                        column: x => x.ZahtjevKomentarId,
                        principalTable: "ZahtjevKomentari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KorisnikKategorije",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KorisnickoIme = table.Column<string>(nullable: true),
                    ZahtjevKategorijaId = table.Column<int>(nullable: false),
                    KorisnikProjekatId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnikKategorije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KorisnikKategorije_Korisnici_KorisnickoIme",
                        column: x => x.KorisnickoIme,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnickoIme",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KorisnikKategorije_KorisnikProjekti_KorisnikProjekatId",
                        column: x => x.KorisnikProjekatId,
                        principalTable: "KorisnikProjekti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KorisnikKategorije_ZahtjevKategorije_ZahtjevKategorijaId",
                        column: x => x.ZahtjevKategorijaId,
                        principalTable: "ZahtjevKategorije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zahtjevi_DodijeljeniKorisnikIme",
                table: "Zahtjevi",
                column: "DodijeljeniKorisnikIme");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtjevi_ZahtjevKategorijaId",
                table: "Zahtjevi",
                column: "ZahtjevKategorijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtjevi_ZahtjevPrioritetId",
                table: "Zahtjevi",
                column: "ZahtjevPrioritetId");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtjevi_ZahtjevStatusId",
                table: "Zahtjevi",
                column: "ZahtjevStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtjevi_ZahtjevTipId",
                table: "Zahtjevi",
                column: "ZahtjevTipId");

            migrationBuilder.CreateIndex(
                name: "IX_Projekti_ProjekatKonfiguracijaId",
                table: "Projekti",
                column: "ProjekatKonfiguracijaId");

            migrationBuilder.CreateIndex(
                name: "IX_DijeloviProjekta_ProjekatId",
                table: "DijeloviProjekta",
                column: "ProjekatId");

            migrationBuilder.CreateIndex(
                name: "IX_IzmjeneZahtjeva_KorisnickoIme",
                table: "IzmjeneZahtjeva",
                column: "KorisnickoIme");

            migrationBuilder.CreateIndex(
                name: "IX_IzmjeneZahtjeva_ZahtjevId",
                table: "IzmjeneZahtjeva",
                column: "ZahtjevId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikKategorije_KorisnickoIme",
                table: "KorisnikKategorije",
                column: "KorisnickoIme");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikKategorije_KorisnikProjekatId",
                table: "KorisnikKategorije",
                column: "KorisnikProjekatId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikKategorije_ZahtjevKategorijaId",
                table: "KorisnikKategorije",
                column: "ZahtjevKategorijaId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikNotifikacije_KorisnickoIme",
                table: "KorisnikNotifikacije",
                column: "KorisnickoIme");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikNotifikacije_NotifikacijaId",
                table: "KorisnikNotifikacije",
                column: "NotifikacijaId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikProjekti_KorisnickoIme",
                table: "KorisnikProjekti",
                column: "KorisnickoIme");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikProjekti_ProjekatId",
                table: "KorisnikProjekti",
                column: "ProjekatId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifikacije_ProjekatId",
                table: "Notifikacije",
                column: "ProjekatId");

            migrationBuilder.CreateIndex(
                name: "IX_PrebacivanjaPrava_DoKorisnickogImena",
                table: "PrebacivanjaPrava",
                column: "DoKorisnickogImena");

            migrationBuilder.CreateIndex(
                name: "IX_PrebacivanjaPrava_OdKorisnickogImena",
                table: "PrebacivanjaPrava",
                column: "OdKorisnickogImena");

            migrationBuilder.CreateIndex(
                name: "IX_PriloziKomentar_DokumentId",
                table: "PriloziKomentar",
                column: "DokumentId");

            migrationBuilder.CreateIndex(
                name: "IX_PriloziKomentar_ZahtjevKomentarId",
                table: "PriloziKomentar",
                column: "ZahtjevKomentarId");

            migrationBuilder.CreateIndex(
                name: "IX_PriloziZahtjeva_DokumentId",
                table: "PriloziZahtjeva",
                column: "DokumentId");

            migrationBuilder.CreateIndex(
                name: "IX_PriloziZahtjeva_ZahtjevId",
                table: "PriloziZahtjeva",
                column: "ZahtjevId");

            migrationBuilder.CreateIndex(
                name: "IX_ZahtjevKategorije_DioProjektaId",
                table: "ZahtjevKategorije",
                column: "DioProjektaId");

            migrationBuilder.CreateIndex(
                name: "IX_ZahtjevKomentari_KorisnikKorisnickoIme",
                table: "ZahtjevKomentari",
                column: "KorisnikKorisnickoIme");

            migrationBuilder.CreateIndex(
                name: "IX_ZahtjevKomentari_ZahtjevId",
                table: "ZahtjevKomentari",
                column: "ZahtjevId");

            migrationBuilder.CreateIndex(
                name: "IX_ZahtjevPrioriteti_ProjekatId",
                table: "ZahtjevPrioriteti",
                column: "ProjekatId");

            migrationBuilder.CreateIndex(
                name: "IX_ZahtjevStatusi_ProjekatId",
                table: "ZahtjevStatusi",
                column: "ProjekatId");

            migrationBuilder.CreateIndex(
                name: "IX_ZahtjevTipovi_ProjekatId",
                table: "ZahtjevTipovi",
                column: "ProjekatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projekti_ProjekatKonfiguracija_ProjekatKonfiguracijaId",
                table: "Projekti",
                column: "ProjekatKonfiguracijaId",
                principalTable: "ProjekatKonfiguracija",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Zahtjevi_Korisnici_DodijeljeniKorisnikIme",
                table: "Zahtjevi",
                column: "DodijeljeniKorisnikIme",
                principalTable: "Korisnici",
                principalColumn: "KorisnickoIme",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zahtjevi_ZahtjevKategorije_ZahtjevKategorijaId",
                table: "Zahtjevi",
                column: "ZahtjevKategorijaId",
                principalTable: "ZahtjevKategorije",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zahtjevi_ZahtjevPrioriteti_ZahtjevPrioritetId",
                table: "Zahtjevi",
                column: "ZahtjevPrioritetId",
                principalTable: "ZahtjevPrioriteti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zahtjevi_ZahtjevStatusi_ZahtjevStatusId",
                table: "Zahtjevi",
                column: "ZahtjevStatusId",
                principalTable: "ZahtjevStatusi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zahtjevi_ZahtjevTipovi_ZahtjevTipId",
                table: "Zahtjevi",
                column: "ZahtjevTipId",
                principalTable: "ZahtjevTipovi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projekti_ProjekatKonfiguracija_ProjekatKonfiguracijaId",
                table: "Projekti");

            migrationBuilder.DropForeignKey(
                name: "FK_Zahtjevi_Korisnici_DodijeljeniKorisnikIme",
                table: "Zahtjevi");

            migrationBuilder.DropForeignKey(
                name: "FK_Zahtjevi_ZahtjevKategorije_ZahtjevKategorijaId",
                table: "Zahtjevi");

            migrationBuilder.DropForeignKey(
                name: "FK_Zahtjevi_ZahtjevPrioriteti_ZahtjevPrioritetId",
                table: "Zahtjevi");

            migrationBuilder.DropForeignKey(
                name: "FK_Zahtjevi_ZahtjevStatusi_ZahtjevStatusId",
                table: "Zahtjevi");

            migrationBuilder.DropForeignKey(
                name: "FK_Zahtjevi_ZahtjevTipovi_ZahtjevTipId",
                table: "Zahtjevi");

            migrationBuilder.DropTable(
                name: "IzmjeneZahtjeva");

            migrationBuilder.DropTable(
                name: "KorisnikKategorije");

            migrationBuilder.DropTable(
                name: "KorisnikNotifikacije");

            migrationBuilder.DropTable(
                name: "PrebacivanjaPrava");

            migrationBuilder.DropTable(
                name: "PriloziKomentar");

            migrationBuilder.DropTable(
                name: "PriloziZahtjeva");

            migrationBuilder.DropTable(
                name: "ProjekatKonfiguracija");

            migrationBuilder.DropTable(
                name: "SifOznakeStatusa");

            migrationBuilder.DropTable(
                name: "ZahtjevPrioriteti");

            migrationBuilder.DropTable(
                name: "ZahtjevStatusi");

            migrationBuilder.DropTable(
                name: "ZahtjevTipovi");

            migrationBuilder.DropTable(
                name: "KorisnikProjekti");

            migrationBuilder.DropTable(
                name: "ZahtjevKategorije");

            migrationBuilder.DropTable(
                name: "Notifikacije");

            migrationBuilder.DropTable(
                name: "ZahtjevKomentari");

            migrationBuilder.DropTable(
                name: "DijeloviProjekta");

            migrationBuilder.DropIndex(
                name: "IX_Zahtjevi_DodijeljeniKorisnikIme",
                table: "Zahtjevi");

            migrationBuilder.DropIndex(
                name: "IX_Zahtjevi_ZahtjevKategorijaId",
                table: "Zahtjevi");

            migrationBuilder.DropIndex(
                name: "IX_Zahtjevi_ZahtjevPrioritetId",
                table: "Zahtjevi");

            migrationBuilder.DropIndex(
                name: "IX_Zahtjevi_ZahtjevStatusId",
                table: "Zahtjevi");

            migrationBuilder.DropIndex(
                name: "IX_Zahtjevi_ZahtjevTipId",
                table: "Zahtjevi");

            migrationBuilder.DropIndex(
                name: "IX_Projekti_ProjekatKonfiguracijaId",
                table: "Projekti");

            migrationBuilder.DropColumn(
                name: "DodijeljeniKorisnikIme",
                table: "Zahtjevi");

            migrationBuilder.DropColumn(
                name: "EstimiranoVrijeme",
                table: "Zahtjevi");

            migrationBuilder.DropColumn(
                name: "PotrošenoVrijeme",
                table: "Zahtjevi");

            migrationBuilder.DropColumn(
                name: "ZahtjevKategorijaId",
                table: "Zahtjevi");

            migrationBuilder.DropColumn(
                name: "ZahtjevPrioritetId",
                table: "Zahtjevi");

            migrationBuilder.DropColumn(
                name: "ZahtjevStatusId",
                table: "Zahtjevi");

            migrationBuilder.DropColumn(
                name: "ZahtjevTipId",
                table: "Zahtjevi");

            migrationBuilder.DropColumn(
                name: "ProjekatKonfiguracijaId",
                table: "Projekti");

            migrationBuilder.AddColumn<int>(
                name: "KomentarId",
                table: "Dokumenti",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Komentari",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
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

            migrationBuilder.AddForeignKey(
                name: "FK_Dokumenti_Komentari_KomentarId",
                table: "Dokumenti",
                column: "KomentarId",
                principalTable: "Komentari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
