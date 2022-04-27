using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lanre.Context.Library.Infrastructure.Database.Migrations
{
    public partial class Add_MarkBook_And_Book_Relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Authors",
                schema: "library",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                schema: "library",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pages",
                schema: "library",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rating",
                schema: "library",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Series",
                schema: "library",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sinopsis",
                schema: "library",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                schema: "library",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "library",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MarkBooks",
                schema: "library",
                columns: table => new
                {
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Marked = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkBooks", x => new { x.BookId, x.UserId, x.Marked });
                    table.ForeignKey(
                        name: "FK_MarkBooks_Books_BookId",
                        column: x => x.BookId,
                        principalSchema: "library",
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "library",
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("332fb5ab-2eab-4393-a920-9b46faed3cb5"),
                columns: new[] { "Authors", "ImageUrl", "Pages", "Rating", "Series", "Sinopsis", "Url", "UserId" },
                values: new object[] { "Orson Scott Card", "http://google.es", "XXX", "5", "Ender", "", "http://google.es", "0000000000" });

            migrationBuilder.UpdateData(
                schema: "library",
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("8bddba00-f200-402d-b45b-6f1634a5f622"),
                columns: new[] { "Authors", "ImageUrl", "Pages", "Rating", "Series", "Sinopsis", "Url", "UserId" },
                values: new object[] { "Brandon Sanderson", "http://google.es", "XXX", "5", "Nacidos de la bruma", "", "http://google.es", "0000000000" });

            migrationBuilder.InsertData(
                schema: "library",
                table: "MarkBooks",
                columns: new[] { "BookId", "Marked", "UserId" },
                values: new object[] { new Guid("8bddba00-f200-402d-b45b-6f1634a5f622"), 10, "251963be-2c3e-435f-9da7-a62bec3d508a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarkBooks",
                schema: "library");

            migrationBuilder.DropColumn(
                name: "Authors",
                schema: "library",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                schema: "library",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Pages",
                schema: "library",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Rating",
                schema: "library",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Series",
                schema: "library",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Sinopsis",
                schema: "library",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Url",
                schema: "library",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "library",
                table: "Books");
        }
    }
}
