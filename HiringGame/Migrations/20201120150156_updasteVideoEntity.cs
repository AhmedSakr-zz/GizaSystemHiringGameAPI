using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HiringGame.Migrations
{
    public partial class updasteVideoEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Videos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Videos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeletedById",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Videos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Videos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Videos");
        }
    }
}
