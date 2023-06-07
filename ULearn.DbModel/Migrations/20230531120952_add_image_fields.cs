﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ULearn.DbModel.Migrations
{
    public partial class add_image_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "video",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "lesson",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "video");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "lesson");
        }
    }
}
