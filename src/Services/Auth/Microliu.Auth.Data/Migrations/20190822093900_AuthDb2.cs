using Microsoft.EntityFrameworkCore.Migrations;

namespace Microliu.Auth.DataMySQL.Migrations
{
    public partial class AuthDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Creator",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "IsEnable",
                table: "Position");

            migrationBuilder.AddColumn<int>(
                name: "IsDelete",
                table: "UserPosition",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsEnabled",
                table: "UserPosition",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsDelete",
                table: "Role",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsEnabled",
                table: "Position",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "AuthUser",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IsDelete",
                table: "AuthUser",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsEnabled",
                table: "AuthUser",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "UserPosition");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "UserPosition");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "AuthUser");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "AuthUser");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "AuthUser");

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "Role",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IsDeleted",
                table: "Role",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsEnable",
                table: "Position",
                nullable: false,
                defaultValue: 0);
        }
    }
}
