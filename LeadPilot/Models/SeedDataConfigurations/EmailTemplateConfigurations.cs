using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace LeadPilot.Models.SeedDataConfigurations
{
    public class EmailTemplateConfigurations : IEntityTypeConfiguration<EmailTemplate>
    {
        public void Configure(EntityTypeBuilder<EmailTemplate> builder)
        {
            builder.HasData(
                new EmailTemplate()
                {
                    Id = 1,
                    Name = "Initial",
                    EmailTypeId = 1,
                    Subject = "Quick question about your internal workflows",
                    Body = "<p>Hi@ContactName,</p><p> I came across @FirmName while researching businesses in @City and wanted to reach out.</p><p> Quick question — are you currently exploring ways to improve internal workflows or automate any repetitive processes?</p><p>I build custom tools and automations that help teams save time and reduce manual work.</p><p>No sales pitch — just curious if this is something on your radar.</p><p style = \"margin-top:25px;\">Best regards,  <br/>Krunal</p>",
                    Active = true,
                    SouceId = null
                },
                new EmailTemplate()
                {
                    Id = 3,
                    Name = "FollowUp",
                    EmailTypeId = 2,
                    Subject = "Following up",
                    Body = "<p>Hi@ContactName,</p>\r\n\r\n<p>Just following up on my earlier note.</p>\r\n\r\n<p>If improving internal workflows or operational efficiency is on your roadmap, I’d be happy to explore whether there’s a good fit.</p>\r\n\r\n<p style=\"margin-top:25px;\">\r\nBest regards,<br/>\r\nKrunal Pagdar<br/>\r\nFull-Stack Developer (.NET | Azure)\r\n</p>",
                    Active = true,
                    SouceId = null
                }
            );
        }
    }
}
