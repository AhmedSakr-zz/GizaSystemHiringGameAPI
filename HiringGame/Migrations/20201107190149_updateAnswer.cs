using Microsoft.EntityFrameworkCore.Migrations;

namespace HiringGame.Migrations
{
    public partial class updateAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "Answers");

            migrationBuilder.AddColumn<string>(
                name: "PersonalityKey",
                table: "Answers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonalityKey",
                table: "Answers");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
