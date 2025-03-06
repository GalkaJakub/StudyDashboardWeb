using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Study_dashboard_API.Migrations
{
    /// <inheritdoc />
    public partial class firstinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Subjects_SubjectId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_UserId",
                table: "Events");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Subjects_SubjectId",
                table: "Events",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_UserId",
                table: "Events",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "Ects", "Name", "PriorityLevel", "UserId" },
                values: new object[,]
                {
                    { 1, 5, "Smiw", 2, 1 },
                    { 2, 2, "Smiw lab", 1, 1 },
                    { 3, 3, "Pk 5", 1, 1 },
                    { 4, 1, "Programowanie 4", 2, 2 },
                    { 5, 2, "PBD", 0, 2 },
                    { 6, 1, "Java_web", 0, 3 },
                    { 7, 1, "Tm", 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Date", "Description", "Name", "PriorityLevel", "SubjectId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nie zdam! xd", "sprawdzian z pic", 2, 1, 1 },
                    { 2, new DateTime(2025, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "wejsciowka z avr", "wejsciowka", 1, 2, 1 },
                    { 3, new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "literaki", "projekt", 2, 3, 1 },
                    { 4, new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nie zdam! xd", "kolokwium", 2, 7, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Subjects_SubjectId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_UserId",
                table: "Events");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Subjects_SubjectId",
                table: "Events",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_UserId",
                table: "Events",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
