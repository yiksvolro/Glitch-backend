using Microsoft.EntityFrameworkCore.Migrations;

namespace Glitch.Migrations
{
    public partial class AddedContactsToPlaces : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Places",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramName",
                table: "Places",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Places",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "InstagramName",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Places");
        }
    }
}
