using Microsoft.EntityFrameworkCore.Migrations;

namespace Riders.Tweakbox.API.Infrastructure.Migrations
{
    public partial class Add_StandardDeviation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Rating",
                table: "RaceDetails",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<float>(
                name: "StdDev",
                table: "RaceDetails",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StdDev",
                table: "RaceDetails");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "RaceDetails",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");
        }
    }
}
