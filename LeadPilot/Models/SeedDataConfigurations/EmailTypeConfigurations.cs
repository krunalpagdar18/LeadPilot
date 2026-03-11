using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace LeadPilot.Models.SeedDataConfigurations
{
    public class EmailTypeConfigurations : IEntityTypeConfiguration<EmailType>
    {
        public void Configure(EntityTypeBuilder<EmailType> builder)
        {
            builder.HasData(
                new EmailType() { Id = 1, Name = "Initial", Active = true },
                new EmailType() { Id = 2, Name = "Followup", Active = true }
           );
        }
    }
}
