using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadPilot.Migrations
{
    /// <inheritdoc />
    public partial class InitialEMailTemplateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Email_Template",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Body", "Name", "SouceID" },
                values: new object[] { "<p>Hi@ContactName,</p>< p > I came across @FirmName while researching businesses in @City and wanted to reach out.</ p >< p > Quick question — are you currently exploring ways to improve internal workflows or automate any repetitive processes?</p><p>I build custom tools and automations that help teams save time and reduce manual work.</p><p>No sales pitch — just curious if this is something on your radar.</p><p style = \"margin-top:25px;\">Best regards,  <br/>Krunal</p>", "Initial", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Email_Template",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Body", "Name", "SouceID" },
                values: new object[] { "<p>Hi@ContactName,</p>\r\n\r\n<p>I came across @FirmName while reviewing accounting firms in @City and wanted to reach out directly. I work with professional services firms to improve internal systems and streamline day-to-day operations.</p>\r\n\r\n<p>If improving internal efficiency or reducing manual effort is something you’re considering this year, would you be open to a short conversation to see if there’s a fit?</p>\r\n\r\n<p style=\"margin-top:25px;\">\r\nBest regards,<br/>\r\nKrunal Pagdar<br/>\r\nFull-Stack Developer (.NET | Azure)\r\n</p>", "GoogleMap - Initial", 1 });
        }
    }
}
