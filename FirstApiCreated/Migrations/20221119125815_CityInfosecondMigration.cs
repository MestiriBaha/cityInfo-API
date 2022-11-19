using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstApiCreated.Migrations
{
    public partial class CityInfosecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pointsOfInterestDto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pointsOfInterestDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CityId = table.Column<int>(type: "INTEGER", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pointsOfInterestDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pointsOfInterestDto_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_pointsOfInterestDto_CityId",
                table: "pointsOfInterestDto",
                column: "CityId");
        }
    }
}
