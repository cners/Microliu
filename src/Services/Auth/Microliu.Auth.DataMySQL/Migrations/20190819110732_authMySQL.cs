using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Microliu.Auth.DataMySQL.Migrations
{
    public partial class authMySQL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    RoleName = table.Column<string>(maxLength: 40, nullable: false),
                    CreateTime = table.Column<DateTimeOffset>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    IsEnabled = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
