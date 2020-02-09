using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Entities.Migrations
{
    public partial class AdministratoruDodanaPravaUpravljanjaNadOstalimKorisnicima : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var query = @"
            declare @administrator int = (select id from Uloge where sifra = 'administrator')
            declare @moderator int = (select id from Uloge where sifra = 'moderator')
            declare @support int = (select id from Uloge where sifra = 'support')
            declare @taskUser int = (select id from Uloge where sifra = 'taskUser')


            if not exists(SELECT * FROM PravaUpravljanjaKorisnicima WHERE UlogaUpraviteljaId=@administrator AND UlogaUpravljanogId=@moderator)
BEGIN
            insert into PravaUpravljanjaKorisnicima ( UlogaUpraviteljaId, UlogaUpravljanogId) values (  @administrator, @moderator)
END
if not exists(SELECT * FROM PravaUpravljanjaKorisnicima WHERE UlogaUpraviteljaId=@administrator AND UlogaUpravljanogId=@support)
BEGIN
            insert into PravaUpravljanjaKorisnicima ( UlogaUpraviteljaId, UlogaUpravljanogId) values (  @administrator, @support)
END
if not exists(SELECT * FROM PravaUpravljanjaKorisnicima WHERE UlogaUpraviteljaId=@administrator AND UlogaUpravljanogId=@taskUser)
BEGIN
            insert into PravaUpravljanjaKorisnicima ( UlogaUpraviteljaId, UlogaUpravljanogId) values (  @administrator, @taskUser)
END";
            migrationBuilder.Sql(query);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
