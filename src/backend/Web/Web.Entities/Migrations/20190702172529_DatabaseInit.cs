using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DatabaseInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dokumenti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Naziv = table.Column<string>(nullable: true),
                    Putanja = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dokumenti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    KorisnickoIme = table.Column<string>(maxLength: 64, nullable: false),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: false),
                    PunoIme = table.Column<string>(maxLength: 128, nullable: false),
                    Tajna = table.Column<byte[]>(nullable: true),
                    Onemogucen = table.Column<bool>(nullable: false),
                    EmailVerifikovan = table.Column<bool>(nullable: false),
                    BrojMobitela = table.Column<string>(nullable: true),
                    UlogaDisc = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.KorisnickoIme);
                });

            migrationBuilder.CreateTable(
                name: "LogKategorije",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    Sifra = table.Column<string>(maxLength: 100, nullable: false),
                    Naziv = table.Column<string>(maxLength: 1000, nullable: false),
                    VrijednostUAplikaciji = table.Column<int>(nullable: true),
                    Poredak = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogKategorije", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogLeveli",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    Sifra = table.Column<string>(maxLength: 100, nullable: false),
                    Naziv = table.Column<string>(maxLength: 1000, nullable: false),
                    VrijednostUAplikaciji = table.Column<int>(nullable: true),
                    Poredak = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogLeveli", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Postavke",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    NaslovSistema = table.Column<string>(maxLength: 64, nullable: false),
                    TrajanjeSesije = table.Column<int>(nullable: false),
                    UrlKarte = table.Column<string>(maxLength: 128, nullable: false),
                    AutorskaPravaKarte = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postavke", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Uloge",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    Sifra = table.Column<string>(maxLength: 100, nullable: false),
                    Naziv = table.Column<string>(maxLength: 1000, nullable: false),
                    VrijednostUAplikaciji = table.Column<int>(nullable: true),
                    Poredak = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uloge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VrsteLogAkcija",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumKreiranja = table.Column<DateTime>(nullable: true),
                    DatumIzmjene = table.Column<DateTime>(nullable: true),
                    Sifra = table.Column<string>(maxLength: 100, nullable: false),
                    Naziv = table.Column<string>(maxLength: 1000, nullable: false),
                    VrijednostUAplikaciji = table.Column<int>(nullable: true),
                    Poredak = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrsteLogAkcija", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogAkcije",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KorisnickoIme = table.Column<string>(maxLength: 64, nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Kategorija = table.Column<int>(nullable: false),
                    Akcija = table.Column<int>(nullable: false),
                    Opis = table.Column<string>(nullable: true),
                    DatumAkcije = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogAkcije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogAkcije_Korisnici_KorisnickoIme",
                        column: x => x.KorisnickoIme,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnickoIme",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LogEntiteti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KorisnickoIme = table.Column<string>(maxLength: 64, nullable: true),
                    EntitetId = table.Column<int>(nullable: false),
                    Entitet = table.Column<int>(nullable: false),
                    Vrijednost = table.Column<string>(nullable: true),
                    DatumAkcije = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEntiteti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogEntiteti_Korisnici_KorisnickoIme",
                        column: x => x.KorisnickoIme,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnickoIme",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tokeni",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    VlasnikKorisnickoIme = table.Column<string>(maxLength: 64, nullable: false),
                    DatumKreiranja = table.Column<DateTime>(nullable: false),
                    DatumIsteka = table.Column<DateTime>(nullable: false),
                    PosljednjiIp = table.Column<string>(maxLength: 256, nullable: true),
                    PosljednjiKlijent = table.Column<string>(maxLength: 256, nullable: true),
                    DatumPosljednjeAkcije = table.Column<DateTime>(nullable: false),
                    Tip = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokeni", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tokeni_Korisnici_VlasnikKorisnickoIme",
                        column: x => x.VlasnikKorisnickoIme,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnickoIme",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_KorisnickoIme",
                table: "Korisnici",
                column: "KorisnickoIme",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_UlogaDisc",
                table: "Korisnici",
                column: "UlogaDisc");

            migrationBuilder.CreateIndex(
                name: "IX_LogAkcije_KorisnickoIme",
                table: "LogAkcije",
                column: "KorisnickoIme");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntiteti_KorisnickoIme",
                table: "LogEntiteti",
                column: "KorisnickoIme");

            migrationBuilder.CreateIndex(
                name: "IX_Tokeni_VlasnikKorisnickoIme",
                table: "Tokeni",
                column: "VlasnikKorisnickoIme");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dokumenti");

            migrationBuilder.DropTable(
                name: "LogAkcije");

            migrationBuilder.DropTable(
                name: "LogEntiteti");

            migrationBuilder.DropTable(
                name: "LogKategorije");

            migrationBuilder.DropTable(
                name: "LogLeveli");

            migrationBuilder.DropTable(
                name: "Postavke");

            migrationBuilder.DropTable(
                name: "Tokeni");

            migrationBuilder.DropTable(
                name: "Uloge");

            migrationBuilder.DropTable(
                name: "VrsteLogAkcija");

            migrationBuilder.DropTable(
                name: "Korisnici");
        }
    }
}
