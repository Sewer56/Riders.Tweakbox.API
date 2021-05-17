﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Riders.Tweakbox.API.Infrastructure.Migrations
{
    public partial class Add_TeamNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamNo",
                table: "RaceDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamNo",
                table: "RaceDetails");
        }
    }
}
