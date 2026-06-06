using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WorldCup2026.Migrations
{
    /// <inheritdoc />
    public partial class CompleteMatchScheduleSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "Id", "AwayScore", "AwayTeamId", "HomeScore", "HomeTeamId", "KickOffTime", "StadiumId", "Status" },
                values: new object[,]
                {
                    { 5, null, 10, null, 9, new DateTime(2026, 6, 13, 18, 0, 0, 0, DateTimeKind.Unspecified), 6, "Scheduled" },
                    { 6, null, 12, null, 11, new DateTime(2026, 6, 13, 22, 0, 0, 0, DateTimeKind.Unspecified), 7, "Scheduled" },
                    { 7, null, 14, null, 13, new DateTime(2026, 6, 14, 15, 0, 0, 0, DateTimeKind.Unspecified), 12, "Scheduled" },
                    { 8, null, 16, null, 15, new DateTime(2026, 6, 14, 19, 0, 0, 0, DateTimeKind.Unspecified), 8, "Scheduled" },
                    { 9, null, 18, null, 17, new DateTime(2026, 6, 14, 23, 0, 0, 0, DateTimeKind.Unspecified), 13, "Scheduled" },
                    { 10, null, 20, null, 19, new DateTime(2026, 6, 15, 16, 0, 0, 0, DateTimeKind.Unspecified), 9, "Scheduled" },
                    { 11, null, 22, null, 21, new DateTime(2026, 6, 15, 20, 0, 0, 0, DateTimeKind.Unspecified), 14, "Scheduled" },
                    { 12, null, 24, null, 23, new DateTime(2026, 6, 15, 23, 59, 0, 0, DateTimeKind.Unspecified), 10, "Scheduled" },
                    { 13, null, 26, null, 25, new DateTime(2026, 6, 16, 15, 0, 0, 0, DateTimeKind.Unspecified), 15, "Scheduled" },
                    { 14, null, 28, null, 27, new DateTime(2026, 6, 16, 19, 0, 0, 0, DateTimeKind.Unspecified), 2, "Scheduled" },
                    { 15, null, 30, null, 29, new DateTime(2026, 6, 16, 23, 0, 0, 0, DateTimeKind.Unspecified), 3, "Scheduled" },
                    { 16, null, 32, null, 31, new DateTime(2026, 6, 17, 16, 0, 0, 0, DateTimeKind.Unspecified), 16, "Scheduled" },
                    { 17, null, 34, null, 33, new DateTime(2026, 6, 17, 20, 0, 0, 0, DateTimeKind.Unspecified), 1, "Scheduled" },
                    { 18, null, 36, null, 35, new DateTime(2026, 6, 17, 23, 59, 0, 0, DateTimeKind.Unspecified), 4, "Scheduled" },
                    { 19, null, 38, null, 37, new DateTime(2026, 6, 18, 18, 0, 0, 0, DateTimeKind.Unspecified), 6, "Scheduled" },
                    { 20, null, 40, null, 39, new DateTime(2026, 6, 18, 22, 0, 0, 0, DateTimeKind.Unspecified), 7, "Scheduled" },
                    { 21, null, 42, null, 41, new DateTime(2026, 6, 18, 23, 59, 0, 0, DateTimeKind.Unspecified), 8, "Scheduled" },
                    { 22, null, 44, null, 43, new DateTime(2026, 6, 19, 15, 0, 0, 0, DateTimeKind.Unspecified), 9, "Scheduled" },
                    { 23, null, 46, null, 45, new DateTime(2026, 6, 19, 19, 0, 0, 0, DateTimeKind.Unspecified), 10, "Scheduled" },
                    { 24, null, 48, null, 47, new DateTime(2026, 6, 19, 23, 0, 0, 0, DateTimeKind.Unspecified), 11, "Scheduled" },
                    { 25, null, 3, null, 1, new DateTime(2026, 6, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), 1, "Scheduled" },
                    { 26, null, 4, null, 2, new DateTime(2026, 6, 20, 20, 0, 0, 0, DateTimeKind.Unspecified), 2, "Scheduled" },
                    { 27, null, 7, null, 5, new DateTime(2026, 6, 20, 23, 59, 0, 0, DateTimeKind.Unspecified), 4, "Scheduled" },
                    { 28, null, 8, null, 6, new DateTime(2026, 6, 21, 15, 0, 0, 0, DateTimeKind.Unspecified), 5, "Scheduled" },
                    { 29, null, 11, null, 9, new DateTime(2026, 6, 21, 19, 0, 0, 0, DateTimeKind.Unspecified), 12, "Scheduled" },
                    { 30, null, 12, null, 10, new DateTime(2026, 6, 21, 23, 0, 0, 0, DateTimeKind.Unspecified), 13, "Scheduled" },
                    { 31, null, 15, null, 13, new DateTime(2026, 6, 22, 16, 0, 0, 0, DateTimeKind.Unspecified), 11, "Scheduled" },
                    { 32, null, 16, null, 14, new DateTime(2026, 6, 22, 20, 0, 0, 0, DateTimeKind.Unspecified), 14, "Scheduled" },
                    { 33, null, 19, null, 17, new DateTime(2026, 6, 22, 23, 59, 0, 0, DateTimeKind.Unspecified), 15, "Scheduled" },
                    { 34, null, 20, null, 18, new DateTime(2026, 6, 23, 15, 0, 0, 0, DateTimeKind.Unspecified), 3, "Scheduled" },
                    { 35, null, 23, null, 21, new DateTime(2026, 6, 23, 19, 0, 0, 0, DateTimeKind.Unspecified), 16, "Scheduled" },
                    { 36, null, 24, null, 22, new DateTime(2026, 6, 23, 23, 0, 0, 0, DateTimeKind.Unspecified), 6, "Scheduled" },
                    { 37, null, 27, null, 25, new DateTime(2026, 6, 24, 16, 0, 0, 0, DateTimeKind.Unspecified), 7, "Scheduled" },
                    { 38, null, 28, null, 26, new DateTime(2026, 6, 24, 20, 0, 0, 0, DateTimeKind.Unspecified), 8, "Scheduled" },
                    { 39, null, 31, null, 29, new DateTime(2026, 6, 24, 23, 59, 0, 0, DateTimeKind.Unspecified), 9, "Scheduled" },
                    { 40, null, 32, null, 30, new DateTime(2026, 6, 25, 15, 0, 0, 0, DateTimeKind.Unspecified), 10, "Scheduled" },
                    { 41, null, 35, null, 33, new DateTime(2026, 6, 25, 19, 0, 0, 0, DateTimeKind.Unspecified), 1, "Scheduled" },
                    { 42, null, 36, null, 34, new DateTime(2026, 6, 25, 23, 0, 0, 0, DateTimeKind.Unspecified), 4, "Scheduled" },
                    { 43, null, 39, null, 37, new DateTime(2026, 6, 26, 16, 0, 0, 0, DateTimeKind.Unspecified), 5, "Scheduled" },
                    { 44, null, 40, null, 38, new DateTime(2026, 6, 26, 20, 0, 0, 0, DateTimeKind.Unspecified), 12, "Scheduled" },
                    { 45, null, 43, null, 41, new DateTime(2026, 6, 26, 23, 59, 0, 0, DateTimeKind.Unspecified), 13, "Scheduled" },
                    { 46, null, 44, null, 42, new DateTime(2026, 6, 27, 15, 0, 0, 0, DateTimeKind.Unspecified), 14, "Scheduled" },
                    { 47, null, 47, null, 45, new DateTime(2026, 6, 27, 19, 0, 0, 0, DateTimeKind.Unspecified), 11, "Scheduled" },
                    { 48, null, 48, null, 46, new DateTime(2026, 6, 27, 23, 0, 0, 0, DateTimeKind.Unspecified), 15, "Scheduled" },
                    { 49, null, 1, null, 4, new DateTime(2026, 6, 28, 18, 0, 0, 0, DateTimeKind.Unspecified), 3, "Scheduled" },
                    { 50, null, 3, null, 2, new DateTime(2026, 6, 28, 18, 0, 0, 0, DateTimeKind.Unspecified), 1, "Scheduled" },
                    { 51, null, 5, null, 8, new DateTime(2026, 6, 28, 22, 0, 0, 0, DateTimeKind.Unspecified), 4, "Scheduled" },
                    { 52, null, 7, null, 6, new DateTime(2026, 6, 28, 22, 0, 0, 0, DateTimeKind.Unspecified), 5, "Scheduled" },
                    { 53, null, 9, null, 12, new DateTime(2026, 6, 29, 18, 0, 0, 0, DateTimeKind.Unspecified), 6, "Scheduled" },
                    { 54, null, 11, null, 10, new DateTime(2026, 6, 29, 18, 0, 0, 0, DateTimeKind.Unspecified), 16, "Scheduled" },
                    { 55, null, 13, null, 16, new DateTime(2026, 6, 29, 22, 0, 0, 0, DateTimeKind.Unspecified), 7, "Scheduled" },
                    { 56, null, 15, null, 14, new DateTime(2026, 6, 29, 22, 0, 0, 0, DateTimeKind.Unspecified), 8, "Scheduled" },
                    { 57, null, 17, null, 20, new DateTime(2026, 6, 30, 18, 0, 0, 0, DateTimeKind.Unspecified), 11, "Scheduled" },
                    { 58, null, 18, null, 19, new DateTime(2026, 6, 30, 18, 0, 0, 0, DateTimeKind.Unspecified), 9, "Scheduled" },
                    { 59, null, 21, null, 24, new DateTime(2026, 6, 30, 22, 0, 0, 0, DateTimeKind.Unspecified), 12, "Scheduled" },
                    { 60, null, 23, null, 22, new DateTime(2026, 6, 30, 22, 0, 0, 0, DateTimeKind.Unspecified), 13, "Scheduled" },
                    { 61, null, 25, null, 28, new DateTime(2026, 7, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), 14, "Scheduled" },
                    { 62, null, 27, null, 26, new DateTime(2026, 7, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), 10, "Scheduled" },
                    { 63, null, 29, null, 32, new DateTime(2026, 7, 1, 22, 0, 0, 0, DateTimeKind.Unspecified), 15, "Scheduled" },
                    { 64, null, 30, null, 31, new DateTime(2026, 7, 1, 22, 0, 0, 0, DateTimeKind.Unspecified), 2, "Scheduled" },
                    { 65, null, 33, null, 36, new DateTime(2026, 7, 2, 18, 0, 0, 0, DateTimeKind.Unspecified), 1, "Scheduled" },
                    { 66, null, 35, null, 34, new DateTime(2026, 7, 2, 18, 0, 0, 0, DateTimeKind.Unspecified), 4, "Scheduled" },
                    { 67, null, 37, null, 40, new DateTime(2026, 7, 2, 22, 0, 0, 0, DateTimeKind.Unspecified), 5, "Scheduled" },
                    { 68, null, 39, null, 38, new DateTime(2026, 7, 2, 22, 0, 0, 0, DateTimeKind.Unspecified), 6, "Scheduled" },
                    { 69, null, 41, null, 44, new DateTime(2026, 7, 3, 18, 0, 0, 0, DateTimeKind.Unspecified), 11, "Scheduled" },
                    { 70, null, 42, null, 43, new DateTime(2026, 7, 3, 18, 0, 0, 0, DateTimeKind.Unspecified), 16, "Scheduled" },
                    { 71, null, 45, null, 48, new DateTime(2026, 7, 3, 22, 0, 0, 0, DateTimeKind.Unspecified), 7, "Scheduled" },
                    { 72, null, 47, null, 46, new DateTime(2026, 7, 3, 22, 0, 0, 0, DateTimeKind.Unspecified), 8, "Scheduled" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 72);
        }
    }
}
