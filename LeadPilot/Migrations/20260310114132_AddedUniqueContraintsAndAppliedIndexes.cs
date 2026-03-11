using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeadPilot.Migrations
{
    /// <inheritdoc />
    public partial class AddedUniqueContraintsAndAppliedIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lead_SourceID",
                table: "Lead");

            migrationBuilder.DropIndex(
                name: "IX_Lead_StatusID",
                table: "Lead");

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

            migrationBuilder.DeleteData(
                table: "Email_Type",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Email_Type",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.CreateIndex(
                name: "Idx_Addedon",
                table: "Lead",
                column: "AddedOn",
                descending: new bool[0])
                .Annotation("Npgsql:StorageParameter:deduplicate_items", "true");

            migrationBuilder.CreateIndex(
                name: "IDX_Inactive",
                table: "Lead",
                column: "Inactive")
                .Annotation("Npgsql:StorageParameter:deduplicate_items", "true");

            migrationBuilder.CreateIndex(
                name: "idx_Source",
                table: "Lead",
                column: "SourceID")
                .Annotation("Npgsql:StorageParameter:deduplicate_items", "true");

            migrationBuilder.CreateIndex(
                name: "IDX_Status",
                table: "Lead",
                column: "StatusID")
                .Annotation("Npgsql:StorageParameter:deduplicate_items", "true");

            migrationBuilder.CreateIndex(
                name: "Uni_Website",
                table: "Lead",
                column: "Website",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Idx_Addedon",
                table: "Lead");

            migrationBuilder.DropIndex(
                name: "IDX_Inactive",
                table: "Lead");

            migrationBuilder.DropIndex(
                name: "idx_Source",
                table: "Lead");

            migrationBuilder.DropIndex(
                name: "IDX_Status",
                table: "Lead");

            migrationBuilder.DropIndex(
                name: "Uni_Website",
                table: "Lead");

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

            migrationBuilder.InsertData(
                table: "Email_Template",
                columns: new[] { "ID", "Active", "Body", "EmailTypeID", "Name", "SouceID", "Subject" },
                values: new object[,]
                {
                    { 1, true, "<p>Hi@ContactName,</p><p> I came across @FirmName while researching businesses in @City and wanted to reach out.</p><p> Quick question — are you currently exploring ways to improve internal workflows or automate any repetitive processes?</p><p>I build custom tools and automations that help teams save time and reduce manual work.</p><p>No sales pitch — just curious if this is something on your radar.</p><p style = \"margin-top:25px;\">Best regards,  <br/>Krunal</p>", 1, "Initial", null, "Quick question about your internal workflows" },
                    { 3, true, "<p>Hi@ContactName,</p>\r\n\r\n<p>Just following up on my earlier note.</p>\r\n\r\n<p>If improving internal workflows or operational efficiency is on your roadmap, I’d be happy to explore whether there’s a good fit.</p>\r\n\r\n<p style=\"margin-top:25px;\">\r\nBest regards,<br/>\r\nKrunal Pagdar<br/>\r\nFull-Stack Developer (.NET | Azure)\r\n</p>", 2, "FollowUp", null, "Following up" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lead_SourceID",
                table: "Lead",
                column: "SourceID");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_StatusID",
                table: "Lead",
                column: "StatusID");
        }
    }
}
