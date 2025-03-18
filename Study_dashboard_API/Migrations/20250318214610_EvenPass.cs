using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Study_dashboard_API.Migrations
{
    /// <inheritdoc />
    public partial class EvenPass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IaActive",
                table: "Events",
                newName: "IsPassed");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                column: "IsPassed",
                value: false);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 2,
                column: "IsPassed",
                value: false);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 3,
                column: "IsPassed",
                value: false);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 4,
                column: "IsPassed",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPassed",
                table: "Events",
                newName: "IaActive");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                column: "IaActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 2,
                column: "IaActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 3,
                column: "IaActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 4,
                column: "IaActive",
                value: true);
        }
    }
}
