using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstApiCreated.Migrations
{
    public partial class CityInfofifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "Description", "Name" },
                values: new object[] { 1, "The City where Bouha came from", "Mahdia" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "Description", "Name" },
                values: new object[] { 2, "The City where Boul3ez came from ", "Tozeur" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "Description", "Name" },
                values: new object[] { 3, "The city where the Animal Harry came from", "Bizete" });

            migrationBuilder.InsertData(
                table: "PointsOfInterest",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 1, 1, "Blue lagoon beach", "The beach" });

            migrationBuilder.InsertData(
                table: "PointsOfInterest",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 2, 2, "The most Magical sahara in Africa ", "Sahara" });

            migrationBuilder.InsertData(
                table: "PointsOfInterest",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 3, 3, "wonderful place ", "Ras jabl" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PointsOfInterest",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PointsOfInterest",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PointsOfInterest",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 3);
        }
    }
}
