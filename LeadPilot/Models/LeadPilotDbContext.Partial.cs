using Microsoft.EntityFrameworkCore;

namespace LeadPilot.Models
{
    public partial class LeadPilotDbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LeadPilotDbContext).Assembly);
        }
    }
}
