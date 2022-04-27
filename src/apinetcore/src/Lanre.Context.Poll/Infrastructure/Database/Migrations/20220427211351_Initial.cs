using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lanre.Context.Poll.Infrastructure.Database.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Poll");

            migrationBuilder.CreateTable(
                name: "VoteList",
                schema: "Poll",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteList", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "Poll",
                table: "VoteList",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[] { new Guid("332fb5ab-2eab-4393-a920-9b46faed3cb5"), "June 2022 Poll", "0000000000" });

            migrationBuilder.InsertData(
                schema: "Poll",
                table: "VoteList",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[] { new Guid("8bddba00-f200-402d-b45b-6f1634a5f622"), "May 2022 Poll", "0000000000" });

            migrationBuilder.CreateIndex(
                name: "IX_VoteList_Name",
                schema: "Poll",
                table: "VoteList",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoteList",
                schema: "Poll");
        }
    }
}
