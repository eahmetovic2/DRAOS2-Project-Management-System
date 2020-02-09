using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DodanaKolonaDodijeljeniKorisnikImeUIzmjeneZahtjeva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DodijeljeniKorisnikIme",
                table: "IzmjeneZahtjeva",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IzmjeneZahtjeva_DodijeljeniKorisnikIme",
                table: "IzmjeneZahtjeva",
                column: "DodijeljeniKorisnikIme");

            migrationBuilder.AddForeignKey(
                name: "FK_IzmjeneZahtjeva_Korisnici_DodijeljeniKorisnikIme",
                table: "IzmjeneZahtjeva",
                column: "DodijeljeniKorisnikIme",
                principalTable: "Korisnici",
                principalColumn: "KorisnickoIme",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IzmjeneZahtjeva_Korisnici_DodijeljeniKorisnikIme",
                table: "IzmjeneZahtjeva");

            migrationBuilder.DropIndex(
                name: "IX_IzmjeneZahtjeva_DodijeljeniKorisnikIme",
                table: "IzmjeneZahtjeva");

            migrationBuilder.DropColumn(
                name: "DodijeljeniKorisnikIme",
                table: "IzmjeneZahtjeva");
        }
    }
}
