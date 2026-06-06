using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorldCup2026.Migrations
{
    /// <inheritdoc />
    public partial class AddKnockoutPredictionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KnockoutPredictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    WinnerTeamId = table.Column<int>(type: "int", nullable: false),
                    Stage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnockoutPredictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KnockoutPredictions_Teams_WinnerTeamId",
                        column: x => x.WinnerTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KnockoutPredictions_WinnerTeamId",
                table: "KnockoutPredictions",
                column: "WinnerTeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KnockoutPredictions");
        }
    }
}
