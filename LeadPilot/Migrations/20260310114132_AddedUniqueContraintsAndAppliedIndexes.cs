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
