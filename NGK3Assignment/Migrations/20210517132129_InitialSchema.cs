using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NGK3Assignment.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherStations",
                columns: table => new
                {
                    PlaceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                name: "WeatherStations");
        }
    }
}
