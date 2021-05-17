using Microsoft.EntityFrameworkCore.Migrations;

namespace Riders.Tweakbox.API.Infrastructure.Migrations
{
    public partial class Add_Ratings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "RatingSolo",
                table: "AspNetUsers",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<float>(
                name: "RatingCustom",
                table: "AspNetUsers",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<float>(
                name: "Rating4v4",
                table: "AspNetUsers",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<float>(
                name: "Rating3v3",
                table: "AspNetUsers",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<float>(
                name: "Rating2v2v2v2",
                table: "AspNetUsers",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<float>(
                name: "Rating2v2v2",
                table: "AspNetUsers",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<float>(
                name: "Rating2v2",
                table: "AspNetUsers",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<float>(
                name: "Rating1v1",
                table: "AspNetUsers",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<float>(
                name: "StandardDeviation1v1",
                table: "AspNetUsers",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "StandardDeviation2v2",
                table: "AspNetUsers",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "StandardDeviation2v2v2",
                table: "AspNetUsers",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "StandardDeviation2v2v2v2",
                table: "AspNetUsers",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "StandardDeviation3v3",
                table: "AspNetUsers",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "StandardDeviation4v4",
                table: "AspNetUsers",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "StandardDeviationCustom",
                table: "AspNetUsers",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "StandardDeviationSolo",
                table: "AspNetUsers",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StandardDeviation1v1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StandardDeviation2v2",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StandardDeviation2v2v2",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StandardDeviation2v2v2v2",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StandardDeviation3v3",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StandardDeviation4v4",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StandardDeviationCustom",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StandardDeviationSolo",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<short>(
                name: "RatingSolo",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AlterColumn<short>(
                name: "RatingCustom",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AlterColumn<short>(
                name: "Rating4v4",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AlterColumn<short>(
                name: "Rating3v3",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AlterColumn<short>(
                name: "Rating2v2v2v2",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AlterColumn<short>(
                name: "Rating2v2v2",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AlterColumn<short>(
                name: "Rating2v2",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AlterColumn<short>(
                name: "Rating1v1",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");
        }
    }
}
