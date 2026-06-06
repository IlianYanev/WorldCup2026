using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorldCup2026.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTableAndAdminSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$6pD4R7Zp4M1i8gE6fXm9UeZ8G5vK0T4vJ7yL0f6R2z4A5wS6n3B6e");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$wFNLyOQSt6Ydzlh/YdFoD.j2/W1F3AhUSyYdCOpcGB4zLnsmLBW12");
        }
    }
}
