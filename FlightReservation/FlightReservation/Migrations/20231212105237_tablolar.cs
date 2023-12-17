using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebProje2023.Migrations
{
    public partial class tablolar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    havalimaniAdi = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    sehir = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    havalimaniKisaltma = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ulke = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ucakModel = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    koltukSayisi = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plane", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sehirKalkis = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    sehirVaris = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    havalimaniKalkis = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    havalimaniVaris = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    biletFiyat = table.Column<int>(type: "integer", nullable: false),
                    tarihKalkis = table.Column<DateTime>(type: "Date", nullable: false),
                    ucakModel = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    kalkisSaat = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    varisSaat = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    havalimaniKalkis = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    havalimaniVaris = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    sehirKalkis = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    sehirVaris = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    yolcuAdi = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    yolcuSoyadi = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ucakModel = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    koltukNo = table.Column<string>(type: "text", nullable: false),
                    saatKalkis = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    saatVaris = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    kullaniciId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Airport");

            migrationBuilder.DropTable(
                name: "Plane");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Ticket");
        }
    }
}
