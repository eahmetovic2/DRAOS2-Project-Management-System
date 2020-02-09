using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DodatniAtributiZaZahtjevKomentarTabelu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZahtjevKomentari_Korisnici_KorisnikKorisnickoIme",
                table: "ZahtjevKomentari");

            migrationBuilder.DropIndex(
                name: "IX_ZahtjevKomentari_KorisnikKorisnickoIme",
                table: "ZahtjevKomentari");

            migrationBuilder.DropColumn(
                name: "KorisnikId",
                table: "ZahtjevKomentari");

            migrationBuilder.RenameColumn(
                name: "KorisnikKorisnickoIme",
                table: "ZahtjevKomentari",
                newName: "ModifiedBy");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "ZahtjevKomentari",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ZahtjevKomentari",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumIzmjene",
                table: "ZahtjevKomentari",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumKreiranja",
                table: "ZahtjevKomentari",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ZahtjevKomentari");

            migrationBuilder.DropColumn(
                name: "DatumIzmjene",
                table: "ZahtjevKomentari");

            migrationBuilder.DropColumn(
                name: "DatumKreiranja",
                table: "ZahtjevKomentari");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "ZahtjevKomentari",
                newName: "KorisnikKorisnickoIme");

            migrationBuilder.AlterColumn<string>(
                name: "KorisnikKorisnickoIme",
                table: "ZahtjevKomentari",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KorisnikId",
                table: "ZahtjevKomentari",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ZahtjevKomentari_KorisnikKorisnickoIme",
                table: "ZahtjevKomentari",
                column: "KorisnikKorisnickoIme");

            migrationBuilder.AddForeignKey(
                name: "FK_ZahtjevKomentari_Korisnici_KorisnikKorisnickoIme",
                table: "ZahtjevKomentari",
                column: "KorisnikKorisnickoIme",
                principalTable: "Korisnici",
                principalColumn: "KorisnickoIme",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
