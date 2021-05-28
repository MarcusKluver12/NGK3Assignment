using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NGK3Assignment.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true),
                    PwHash = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "WeatherStations",
                columns: table => new
                {
                    PlaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lat = table.Column<double>(type: "float", nullable: false),
                    lon = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Celcius = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Humidity = table.Column<double>(type: "float", nullable: false),
                    Airpressure = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherStations", x => x.PlaceId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WeatherStations");
        }
    }
}
