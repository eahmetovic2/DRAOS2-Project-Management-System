using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class DodanaKolonaKorisnikUlogaIdNaProjekat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KorisnikProjekti_Korisnici_KorisnickoIme",
                table: "KorisnikProjekti");

            migrationBuilder.DropForeignKey(
                name: "FK_KorisnikUlogaDodatneInformacije_KorisnikUloge_KorisnickoIme_UlogaId",
                table: "KorisnikUlogaDodatneInformacije");

            migrationBuilder.DropForeignKey(
                name: "FK_KorisnikUloge_Korisnici_KorisnickoIme",
                table: "KorisnikUloge");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_KorisnikUloge_KorisnikUlogaId",
                table: "KorisnikUloge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KorisnikUloge",
                table: "KorisnikUloge");

            migrationBuilder.DropIndex(
                name: "IX_KorisnikUlogaDodatneInformacije_KorisnickoIme_UlogaId",
                table: "KorisnikUlogaDodatneInformacije");

            migrationBuilder.DropColumn(
                name: "KorisnickoIme",
                table: "KorisnikUlogaDodatneInformacije");

            migrationBuilder.RenameColumn(
                name: "UlogaId",
                table: "KorisnikUlogaDodatneInformacije",
                newName: "KorisnikUlogaId");

            migrationBuilder.DropIndex(
                name: "IX_KorisnikProjekti_KorisnickoIme",
                table: "KorisnikProjekti");

            migrationBuilder.DropColumn(
                name: "KorisnickoIme",
                table: "KorisnikProjekti");

            migrationBuilder.AlterColumn<string>(
                name: "KorisnickoIme",
                table: "KorisnikUloge",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 64);

            migrationBuilder.AddColumn<int>(
                name: "KorisnikUlogaId",
                table: "KorisnikProjekti",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_KorisnikUloge",
                table: "KorisnikUloge",
                column: "KorisnikUlogaId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikUloge_KorisnickoIme",
                table: "KorisnikUloge",
                column: "KorisnickoIme");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikUlogaDodatneInformacije_KorisnikUlogaId",
                table: "KorisnikUlogaDodatneInformacije",
                column: "KorisnikUlogaId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikProjekti_KorisnikUlogaId",
                table: "KorisnikProjekti",
                column: "KorisnikUlogaId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_KorisnikProjekti_Korisnici_KorisnikKorisnickoIme",
            //    table: "KorisnikProjekti",
            //    column: "KorisnikKorisnickoIme",
            //    principalTable: "Korisnici",
            //    principalColumn: "KorisnickoIme",
            //    onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnikProjekti_KorisnikUloge_KorisnikUlogaId",
                table: "KorisnikProjekti",
                column: "KorisnikUlogaId",
                principalTable: "KorisnikUloge",
                principalColumn: "KorisnikUlogaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnikUlogaDodatneInformacije_KorisnikUloge_KorisnikUlogaId",
                table: "KorisnikUlogaDodatneInformacije",
                column: "KorisnikUlogaId",
                principalTable: "KorisnikUloge",
                principalColumn: "KorisnikUlogaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnikUloge_Korisnici_KorisnickoIme",
                table: "KorisnikUloge",
                column: "KorisnickoIme",
                principalTable: "Korisnici",
                principalColumn: "KorisnickoIme",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KorisnikProjekti_Korisnici_KorisnikKorisnickoIme",
                table: "KorisnikProjekti");

            migrationBuilder.DropForeignKey(
                name: "FK_KorisnikProjekti_KorisnikUloge_KorisnikUlogaId",
                table: "KorisnikProjekti");

            migrationBuilder.DropForeignKey(
                name: "FK_KorisnikUlogaDodatneInformacije_KorisnikUloge_KorisnikUlogaId",
                table: "KorisnikUlogaDodatneInformacije");

            migrationBuilder.DropForeignKey(
                name: "FK_KorisnikUloge_Korisnici_KorisnickoIme",
                table: "KorisnikUloge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KorisnikUloge",
                table: "KorisnikUloge");

            migrationBuilder.DropIndex(
                name: "IX_KorisnikUloge_KorisnickoIme",
                table: "KorisnikUloge");

            migrationBuilder.DropIndex(
                name: "IX_KorisnikUlogaDodatneInformacije_KorisnikUlogaId",
                table: "KorisnikUlogaDodatneInformacije");

            migrationBuilder.DropIndex(
                name: "IX_KorisnikProjekti_KorisnikUlogaId",
                table: "KorisnikProjekti");

            migrationBuilder.DropColumn(
                name: "KorisnikUlogaId",
                table: "KorisnikProjekti");

            migrationBuilder.RenameColumn(
                name: "KorisnikUlogaId",
                table: "KorisnikUlogaDodatneInformacije",
                newName: "UlogaId");

            migrationBuilder.RenameColumn(
                name: "KorisnikKorisnickoIme",
                table: "KorisnikProjekti",
                newName: "KorisnickoIme");

            migrationBuilder.RenameIndex(
                name: "IX_KorisnikProjekti_KorisnikKorisnickoIme",
                table: "KorisnikProjekti",
                newName: "IX_KorisnikProjekti_KorisnickoIme");

            migrationBuilder.AlterColumn<string>(
                name: "KorisnickoIme",
                table: "KorisnikUloge",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KorisnickoIme",
                table: "KorisnikUlogaDodatneInformacije",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_KorisnikUloge_KorisnikUlogaId",
                table: "KorisnikUloge",
                column: "KorisnikUlogaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KorisnikUloge",
                table: "KorisnikUloge",
                columns: new[] { "KorisnickoIme", "UlogaId" });

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikUlogaDodatneInformacije_KorisnickoIme_UlogaId",
                table: "KorisnikUlogaDodatneInformacije",
                columns: new[] { "KorisnickoIme", "UlogaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnikProjekti_Korisnici_KorisnickoIme",
                table: "KorisnikProjekti",
                column: "KorisnickoIme",
                principalTable: "Korisnici",
                principalColumn: "KorisnickoIme",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnikUlogaDodatneInformacije_KorisnikUloge_KorisnickoIme_UlogaId",
                table: "KorisnikUlogaDodatneInformacije",
                columns: new[] { "KorisnickoIme", "UlogaId" },
                principalTable: "KorisnikUloge",
                principalColumns: new[] { "KorisnickoIme", "UlogaId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnikUloge_Korisnici_KorisnickoIme",
                table: "KorisnikUloge",
                column: "KorisnickoIme",
                principalTable: "Korisnici",
                principalColumn: "KorisnickoIme",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
