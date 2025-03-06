using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Study_dashboard_API.Migrations
{
    /// <inheritdoc />
    public partial class SubjectAndEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectId1",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "SubjectId1", "UserId1" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 2,
                columns: new[] { "SubjectId1", "UserId1" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 3,
                columns: new[] { "SubjectId1", "UserId1" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 4,
                columns: new[] { "SubjectId1", "UserId1" },
                values: new object[] { null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Events_SubjectId1",
                table: "Events",
                column: "SubjectId1");

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserId1",
                table: "Events",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Subjects_SubjectId1",
                table: "Events",
                column: "SubjectId1",
                principalTable: "Subjects",
                principalColumn: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_UserId1",
                table: "Events",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Subjects_SubjectId1",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_UserId1",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_SubjectId1",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_UserId1",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "SubjectId1",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Events");
        }
    }
}
