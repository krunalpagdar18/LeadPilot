using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeadPilot.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Email_Type",
                columns: new[] { "ID", "Active", "Name" },
                values: new object[,]
                {
                    { 1, true, "Initial" },
                    { 2, true, "Followup" }
                });

            migrationBuilder.InsertData(
                table: "Lead_Source",
                columns: new[] { "ID", "Active", "Name" },
                values: new object[,]
                {
                    { 1, true, "Google Map" },
                    { 2, true, "HotFrog" }
                });

            migrationBuilder.InsertData(
                table: "Lead_Status",
                columns: new[] { "ID", "Active", "Name" },
                values: new object[,]
                {
                    { 6, true, "New" },
                    { 7, true, "InitialSent" },
                    { 8, true, "FollowUpSent" },
                    { 9, true, "Replied" },
                    { 10, true, "Closed" },
                    { 11, true, "DoNotContact" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Email_Type",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Email_Type",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Lead_Source",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Lead_Source",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Lead_Status",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Lead_Status",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Lead_Status",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Lead_Status",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Lead_Status",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Lead_Status",
                keyColumn: "ID",
                keyValue: 11);
        }
    }
}
