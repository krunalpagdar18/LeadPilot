using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadPilot.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedInitialEmailTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Email_Template",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Email_Template",
                keyColumn: "ID",
                keyValue: 1,
                column: "Body",
                value: "<p>Hi@ContactName,</p><p> I came across @FirmName while researching businesses in @City and wanted to reach out.</p><p> Quick question — are you currently exploring ways to improve internal workflows or automate any repetitive processes?</p><p>I build custom tools and automations that help teams save time and reduce manual work.</p><p>No sales pitch — just curious if this is something on your radar.</p><p style = \"margin-top:25px;\">Best regards,  <br/>Krunal</p>");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Email_Template",
                keyColumn: "ID",
                keyValue: 1,
                column: "Body",
                value: "<p>Hi@ContactName,</p>< p > I came across @FirmName while researching businesses in @City and wanted to reach out.</ p >< p > Quick question — are you currently exploring ways to improve internal workflows or automate any repetitive processes?</p><p>I build custom tools and automations that help teams save time and reduce manual work.</p><p>No sales pitch — just curious if this is something on your radar.</p><p style = \"margin-top:25px;\">Best regards,  <br/>Krunal</p>");

            migrationBuilder.InsertData(
                table: "Email_Template",
                columns: new[] { "ID", "Body", "EmailTypeID", "Name", "SouceID", "Subject" },
                values: new object[] { 2, "<p>Hi@ContactName,</p>\r\n\r\n<p>I came across @FirmName while reviewing accounting firms listed on Hotfrog and wanted to reach out directly. I work with professional services firms to improve internal systems and streamline day-to-day operations.</p>\r\n\r\n<p>If improving internal efficiency is something you’re exploring this year, I’d be happy to have a quick conversation.</p>\r\n\r\n<p style=\"margin-top:25px;\">\r\nBest regards,<br/>\r\nKrunal Pagdar<br/>\r\nFull-Stack Developer (.NET | Azure)\r\n</p>", 1, "HotFrog - Initial", 2, "Quick question after finding @FirmName on Hotfrog" });
        }
    }
}
