using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace LeadPilot.Models.SeedDataConfigurations
{
    public class LeadStatusConfigurations : IEntityTypeConfiguration<LeadStatus>
    {
        public void Configure(EntityTypeBuilder<LeadStatus> builder)
        {
            builder.HasData(
                new LeadStatus() { Id = 6, Name = "New", Active = true },
                new LeadStatus() { Id = 7, Name = "InitialSent", Active = true },
                new LeadStatus() { Id = 8, Name = "FollowUpSent", Active = true },
                new LeadStatus() { Id = 9, Name = "Replied", Active = true },
                new LeadStatus() { Id = 10, Name = "Closed", Active = true },
                new LeadStatus() { Id = 11, Name = "DoNotContact", Active = true },
                new LeadStatus() { Id = 12, Name = "NotInterested", Active = true }
            );
        }
    }
}
