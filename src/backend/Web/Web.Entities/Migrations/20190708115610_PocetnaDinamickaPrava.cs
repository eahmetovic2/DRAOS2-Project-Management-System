using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.IO;

namespace Web.Entities.Migrations
{
    public partial class PocetnaDinamickaPrava : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            var sqlFile = Path.Combine(baseDirectory, "Migrations/SQLScripts", "DinamickaPrava1.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
