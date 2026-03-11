using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace LeadPilot.Models.SeedDataConfigurations
{
    public class LeadSourceConfigurations : IEntityTypeConfiguration<LeadSource>
    {
        public void Configure(EntityTypeBuilder<LeadSource> builder)
        {
            builder.HasData(
                new LeadSource() { Id = 1, Name = "Google Map", Active = true },
                new LeadSource() { Id = 2, Name = "HotFrog", Active = true }
           );
        }
    }
}
