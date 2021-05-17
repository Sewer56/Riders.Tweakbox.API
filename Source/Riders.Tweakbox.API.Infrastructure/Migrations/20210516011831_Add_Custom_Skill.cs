using Microsoft.EntityFrameworkCore.Migrations;

namespace Riders.Tweakbox.API.Infrastructure.Migrations
{
    public partial class Add_Custom_Skill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "RaceDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<short>(
                name: "RatingCustom",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "RaceDetails");

            migrationBuilder.DropColumn(
                name: "RatingCustom",
                table: "AspNetUsers");
        }
    }
}
