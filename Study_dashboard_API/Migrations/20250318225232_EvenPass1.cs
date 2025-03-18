using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Study_dashboard_API.Migrations
{
    /// <inheritdoc />
    public partial class EvenPass1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Grade",
                table: "Events",
                type: "float",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                column: "Grade",
                value: null);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 2,
                column: "Grade",
                value: null);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 3,
                column: "Grade",
                value: null);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 4,
                column: "Grade",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Events");
        }
    }
}
