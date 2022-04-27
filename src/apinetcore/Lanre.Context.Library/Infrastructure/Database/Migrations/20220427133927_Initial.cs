using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lanre.Context.Library.Infrastructure.Database.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "library");

            migrationBuilder.CreateTable(
                name: "Books",
                schema: "library",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "library",
                table: "Books",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("332fb5ab-2eab-4393-a920-9b46faed3cb5"), "El juego de Ender" });

            migrationBuilder.InsertData(
                schema: "library",
                table: "Books",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("8bddba00-f200-402d-b45b-6f1634a5f622"), "El imperio final" });

            migrationBuilder.CreateIndex(
                name: "IX_Books_Name",
                schema: "library",
                table: "Books",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books",
                schema: "library");
        }
    }
}
