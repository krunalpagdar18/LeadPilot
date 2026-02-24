using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeadPilot.Migrations
{
    /// <inheritdoc />
    public partial class addedEmailTemplates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Email_Template",
                columns: new[] { "ID", "Active", "Body", "EmailTypeID", "Name", "SouceID", "Subject" },
                values: new object[,]
                {
                    { 1, true, "Hi@ContactName,<br/></br>I came across @FirmName while reviewing accounting firms in @City and wanted to reach out directly.I work with professional services firms to improve internal systems and streamline day-to - day operations.If improving internal efficiency or reducing manual effort is something you’re considering this year, would you be open to a short conversation to see if there’s a fit?<br/></br>Best regards,<br/>Krunal Pagdar<br/>Full-Stack Developer (.NET | Azure)", 1, "GoogleMap - Initial", 1, "Quick question about your internal workflows" },
                    { 2, true, "Hi@ContactName,<br/><br/>I came across @FirmName while reviewing accounting firms listed on Hotfrog and wanted to reach out directly.I work with professional services firms to improve internal systems and streamline day-to - day operations.<br/><br/>If improving internal efficiency is something you’re exploring this year, I’d be happy to have a quick conversation.<br/><br/>Best regards,<br/>Krunal Pagdar<br/>Full-Stack Developer (.NET | Azure)", 1, "HotFrog - Initial", 2, "Quick question after finding @FirmName on Hotfrog" },
                    { 3, true, "Hi@ContactName,<br/><br/>Just following up on my earlier note.< br />< br />If improving internal workflows or operational efficiency is on your roadmap, I’d be happy to explore whether there’s a good fit.<br/><br/>Best regards,<br/>Krunal Pagdar<br/>Full-Stack Developer (.NET | Azure)", 2, "FollowUp", null, "Following up" }
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
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Email_Template",
                keyColumn: "ID",
                keyValue: 3);
        }
    }
}
