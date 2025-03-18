using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Study_dashboard_API.Migrations
{
    /// <inheritdoc />
    public partial class SubjectPass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Grade",
                table: "Subjects",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPassed",
                table: "Subjects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 1,
                columns: new[] { "Grade", "IsPassed" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 2,
                columns: new[] { "Grade", "IsPassed" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 3,
                columns: new[] { "Grade", "IsPassed" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 4,
                columns: new[] { "Grade", "IsPassed" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 5,
                columns: new[] { "Grade", "IsPassed" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 6,
                columns: new[] { "Grade", "IsPassed" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Subjects",
                keyColumn: "SubjectId",
                keyValue: 7,
                columns: new[] { "Grade", "IsPassed" },
                values: new object[] { null, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "IsPassed",
                table: "Subjects");
        }
    }
}
