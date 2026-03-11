using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeadPilot.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataConfigurationsForAllEntities : Migration
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
                table: "Email_Template",
                columns: new[] { "ID", "Active", "Body", "EmailTypeID", "Name", "SouceID", "Subject" },
                values: new object[,]
                {
                    { 1, true, "<p>Hi@ContactName,</p><p> I came across @FirmName while researching businesses in @City and wanted to reach out.</p><p> Quick question — are you currently exploring ways to improve internal workflows or automate any repetitive processes?</p><p>I build custom tools and automations that help teams save time and reduce manual work.</p><p>No sales pitch — just curious if this is something on your radar.</p><p style = \"margin-top:25px;\">Best regards,  <br/>Krunal</p>", 1, "Initial", null, "Quick question about your internal workflows" },
                    { 3, true, "<p>Hi@ContactName,</p>\r\n\r\n<p>Just following up on my earlier note.</p>\r\n\r\n<p>If improving internal workflows or operational efficiency is on your roadmap, I’d be happy to explore whether there’s a good fit.</p>\r\n\r\n<p style=\"margin-top:25px;\">\r\nBest regards,<br/>\r\nKrunal Pagdar<br/>\r\nFull-Stack Developer (.NET | Azure)\r\n</p>", 2, "FollowUp", null, "Following up" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Email_Template",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Email_Template",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Lead_Source",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Lead_Source",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Email_Type",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Email_Type",
                keyColumn: "ID",
                keyValue: 2);
        }
    }
}
