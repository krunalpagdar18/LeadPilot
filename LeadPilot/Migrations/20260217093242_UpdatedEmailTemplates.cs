using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadPilot.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEmailTemplates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Email_Template",
                keyColumn: "ID",
                keyValue: 1,
                column: "Body",
                value: "<p>Hi@ContactName,</p>\r\n\r\n<p>I came across @FirmName while reviewing accounting firms in @City and wanted to reach out directly. I work with professional services firms to improve internal systems and streamline day-to-day operations.</p>\r\n\r\n<p>If improving internal efficiency or reducing manual effort is something you’re considering this year, would you be open to a short conversation to see if there’s a fit?</p>\r\n\r\n<p style=\"margin-top:25px;\">\r\nBest regards,<br/>\r\nKrunal Pagdar<br/>\r\nFull-Stack Developer (.NET | Azure)\r\n</p>");

            migrationBuilder.UpdateData(
                table: "Email_Template",
                keyColumn: "ID",
                keyValue: 2,
                column: "Body",
                value: "<p>Hi@ContactName,</p>\r\n\r\n<p>I came across @FirmName while reviewing accounting firms listed on Hotfrog and wanted to reach out directly. I work with professional services firms to improve internal systems and streamline day-to-day operations.</p>\r\n\r\n<p>If improving internal efficiency is something you’re exploring this year, I’d be happy to have a quick conversation.</p>\r\n\r\n<p style=\"margin-top:25px;\">\r\nBest regards,<br/>\r\nKrunal Pagdar<br/>\r\nFull-Stack Developer (.NET | Azure)\r\n</p>");

            migrationBuilder.UpdateData(
                table: "Email_Template",
                keyColumn: "ID",
                keyValue: 3,
                column: "Body",
                value: "<p>Hi@ContactName,</p>\r\n\r\n<p>Just following up on my earlier note.</p>\r\n\r\n<p>If improving internal workflows or operational efficiency is on your roadmap, I’d be happy to explore whether there’s a good fit.</p>\r\n\r\n<p style=\"margin-top:25px;\">\r\nBest regards,<br/>\r\nKrunal Pagdar<br/>\r\nFull-Stack Developer (.NET | Azure)\r\n</p>");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Email_Template",
                keyColumn: "ID",
                keyValue: 1,
                column: "Body",
                value: "Hi@ContactName,<br/></br>I came across @FirmName while reviewing accounting firms in @City and wanted to reach out directly.I work with professional services firms to improve internal systems and streamline day-to - day operations.If improving internal efficiency or reducing manual effort is something you’re considering this year, would you be open to a short conversation to see if there’s a fit?<br/></br>Best regards,<br/>Krunal Pagdar<br/>Full-Stack Developer (.NET | Azure)");

            migrationBuilder.UpdateData(
                table: "Email_Template",
                keyColumn: "ID",
                keyValue: 2,
                column: "Body",
                value: "Hi@ContactName,<br/><br/>I came across @FirmName while reviewing accounting firms listed on Hotfrog and wanted to reach out directly.I work with professional services firms to improve internal systems and streamline day-to - day operations.<br/><br/>If improving internal efficiency is something you’re exploring this year, I’d be happy to have a quick conversation.<br/><br/>Best regards,<br/>Krunal Pagdar<br/>Full-Stack Developer (.NET | Azure)");

            migrationBuilder.UpdateData(
                table: "Email_Template",
                keyColumn: "ID",
                keyValue: 3,
                column: "Body",
                value: "Hi@ContactName,<br/><br/>Just following up on my earlier note.< br />< br />If improving internal workflows or operational efficiency is on your roadmap, I’d be happy to explore whether there’s a good fit.<br/><br/>Best regards,<br/>Krunal Pagdar<br/>Full-Stack Developer (.NET | Azure)");
        }
    }
}
