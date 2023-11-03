using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OlympicGames.Migrations
{
    /// <inheritdoc />
    public partial class Added_Sample_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Competitor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Samples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitorId = table.Column<int>(type: "int", nullable: false),
                    Mode = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Samples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Samples_Competitor_CompetitorId",
                        column: x => x.CompetitorId,
                        principalTable: "Competitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Competitor",
                columns: new[] { "Id", "Country", "FullName" },
                values: new object[,]
                {
                    { 1, "COL", "Anthony Boral" },
                    { 2, "CHN", "Marcela Lopez" },
                    { 3, "AUS", "Alejandra Ortega" }
                });

            migrationBuilder.InsertData(
                table: "Samples",
                columns: new[] { "Id", "CompetitorId", "Mode", "Score" },
                values: new object[,]
                {
                    { 1, 1, 1, 134 },
                    { 2, 1, 1, 130 },
                    { 3, 1, 1, 120 },
                    { 4, 1, 2, 177 },
                    { 5, 1, 2, 100 },
                    { 6, 1, 2, 115 },
                    { 7, 2, 1, 130 },
                    { 8, 2, 1, 90 },
                    { 9, 2, 1, 125 },
                    { 10, 2, 2, 180 },
                    { 11, 2, 2, 112 },
                    { 12, 2, 2, 120 },
                    { 13, 3, 1, 0 },
                    { 14, 3, 1, 0 },
                    { 15, 3, 1, 0 },
                    { 16, 3, 2, 150 },
                    { 17, 3, 2, 149 },
                    { 18, 3, 2, 150 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Samples_CompetitorId",
                table: "Samples",
                column: "CompetitorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Samples");

            migrationBuilder.DropTable(
                name: "Competitor");
        }
    }
}
