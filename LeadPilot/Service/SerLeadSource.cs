using LeadPilot.Models;
using LeadPilot.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LeadPilot.Service
{
    public class SerLeadSource
    {
        private readonly LeadPilotDbContext _context;
        public SerLeadSource(LeadPilotDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseViewModel<List<LeadSource>>> GetLeadSource()
        {
            var leadSource = await _context.LeadSources.AsNoTracking().ToListAsync();
            return new ResponseViewModel<List<LeadSource>>(leadSource);
        }
    }
}
