using Microsoft.EntityFrameworkCore.Migrations;

namespace AppynittyWebApp.Migrations
{
    public partial class PasswordString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordString",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordString",
                table: "AspNetUsers");
        }
    }
}
