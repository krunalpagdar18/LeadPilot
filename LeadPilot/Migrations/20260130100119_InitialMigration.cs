using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LeadPilot.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Email_Type",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Email_Type_pkey", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Lead_Source",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Lead_Source_pkey", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Lead_Status",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Lead_Status_pkey", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Email_Template",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    EmailTypeID = table.Column<int>(type: "integer", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    SouceID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Email_Template_pkey", x => x.ID);
                    table.ForeignKey(
                        name: "Email_Type_Email_Template",
                        column: x => x.EmailTypeID,
                        principalTable: "Email_Type",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "LeadSource_EmailTemplate",
                        column: x => x.SouceID,
                        principalTable: "Lead_Source",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Lead",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    CompanyName = table.Column<string>(type: "text", nullable: false),
                    ContactName = table.Column<string>(type: "text", nullable: true),
                    Website = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: false),
                    SourceID = table.Column<int>(type: "integer", nullable: true),
                    StatusID = table.Column<int>(type: "integer", nullable: true),
                    AddedOn = table.Column<DateOnly>(type: "date", nullable: true),
                    EmailID = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Primary_Key_Lead", x => x.ID);
                    table.ForeignKey(
                        name: "Lead_source",
                        column: x => x.SourceID,
                        principalTable: "Lead_Source",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "Lead_status",
                        column: x => x.StatusID,
                        principalTable: "Lead_Status",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "LeadEmailLog",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    LeadID = table.Column<int>(type: "integer", nullable: false),
                    EmailTemplateID = table.Column<int>(type: "integer", nullable: false),
                    NextEmailDate = table.Column<DateOnly>(type: "date", nullable: true),
                    MailDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("LeadEmailLog_pkey", x => x.ID);
                    table.ForeignKey(
                        name: "Lead_EmailLog",
                        column: x => x.LeadID,
                        principalTable: "Lead",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "Lead_TemplateID",
                        column: x => x.EmailTemplateID,
                        principalTable: "Email_Template",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Email_Template_EmailTypeID",
                table: "Email_Template",
                column: "EmailTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Email_Template_SouceID",
                table: "Email_Template",
                column: "SouceID");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_SourceID",
                table: "Lead",
                column: "SourceID");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_StatusID",
                table: "Lead",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadEmailLog_EmailTemplateID",
                table: "LeadEmailLog",
                column: "EmailTemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadEmailLog_LeadID",
                table: "LeadEmailLog",
                column: "LeadID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadEmailLog");

            migrationBuilder.DropTable(
                name: "Lead");

            migrationBuilder.DropTable(
                name: "Email_Template");

            migrationBuilder.DropTable(
                name: "Lead_Status");

            migrationBuilder.DropTable(
                name: "Email_Type");

            migrationBuilder.DropTable(
                name: "Lead_Source");
        }
    }
}
