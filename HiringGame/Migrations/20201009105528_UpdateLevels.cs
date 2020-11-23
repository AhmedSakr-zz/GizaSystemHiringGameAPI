using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HiringGame.Migrations
{
    public partial class UpdateLevels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Levels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Levels",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeletedById",
                table: "Levels",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Levels",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Levels",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TotalQuestions",
                table: "Levels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Levels",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Levels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "TotalQuestions",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Levels");
        }
    }
}
