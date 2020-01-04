using Microsoft.EntityFrameworkCore.Migrations;

namespace GoPlaces.Migrations
{
    public partial class IncludeProfilePicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureImagePath",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePictureImagePath",
                table: "AspNetUsers");
        }
    }
}
