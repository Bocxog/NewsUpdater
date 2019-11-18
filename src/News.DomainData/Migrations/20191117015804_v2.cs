using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace News.DomainData.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Community_CommunityId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Community");

            migrationBuilder.DropIndex(
                name: "IX_Posts_CommunityId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CommunityId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "LastCheck",
                table: "Posts");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Created",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BannedBy",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedUTC",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Domain",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Downvotes",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Gilded",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Posts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEdited",
                table: "Posts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLiked",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsNSFW",
                table: "Posts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSaved",
                table: "Posts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSelfPost",
                table: "Posts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSpoiler",
                table: "Posts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStickied",
                table: "Posts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Kind",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdate",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MyVote",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Permalink",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReportCount",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SelfText",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SelfTextHtml",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Shortlink",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubRedditId",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Upvotes",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubReddits",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Members = table.Column<int>(nullable: true),
                    CakeDay = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubReddits", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_SubRedditId",
                table: "Posts",
                column: "SubRedditId");

            migrationBuilder.CreateIndex(
                name: "IX_SubReddits_Name",
                table: "SubReddits",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_SubReddits_SubRedditId",
                table: "Posts",
                column: "SubRedditId",
                principalTable: "SubReddits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_SubReddits_SubRedditId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "SubReddits");

            migrationBuilder.DropIndex(
                name: "IX_Posts_SubRedditId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "BannedBy",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CreatedUTC",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Domain",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Downvotes",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Gilded",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsEdited",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsLiked",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsNSFW",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsSaved",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsSelfPost",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsSpoiler",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsStickied",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Kind",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "LastUpdate",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "MyVote",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Permalink",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ReportCount",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SelfText",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SelfTextHtml",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Shortlink",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SubRedditId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Upvotes",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Posts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AddColumn<int>(
                name: "CommunityId",
                table: "Posts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastCheck",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Community",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CakeDay = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Link = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Community", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CommunityId",
                table: "Posts",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_Community_Name",
                table: "Community",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Community_CommunityId",
                table: "Posts",
                column: "CommunityId",
                principalTable: "Community",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
