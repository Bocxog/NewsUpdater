using DomainData.Models;
using Microsoft.EntityFrameworkCore.Migrations;

namespace News.DomainData.Migrations
{
    public partial class v : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<PostHistory[]>(
                name: "HistoryMarks",
                table: "Posts",
                type: "jsonb",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HistoryMarks",
                table: "Posts");
        }
    }
}
