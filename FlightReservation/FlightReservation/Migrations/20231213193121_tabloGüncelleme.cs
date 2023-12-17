using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProje2023.Migrations
{
    public partial class tabloGüncelleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "biletFiyat",
                table: "Ticket",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "tarihKalkis",
                table: "Ticket",
                type: "Date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "biletFiyat",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "tarihKalkis",
                table: "Ticket");
        }
    }
}
