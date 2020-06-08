using Microsoft.EntityFrameworkCore.Migrations;

namespace DncZeus.Api.Migrations
{
    public partial class MyMigration2222 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "thumbUrl",
                table: "WeiXinNews",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "thumbUrl",
                table: "WeiXinKnowledge",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "thumbUrl",
                table: "WeiXinCases",
                type: "varchar(200)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "thumbUrl",
                table: "WeiXinNews");

            migrationBuilder.DropColumn(
                name: "thumbUrl",
                table: "WeiXinKnowledge");

            migrationBuilder.DropColumn(
                name: "thumbUrl",
                table: "WeiXinCases");
        }
    }
}
