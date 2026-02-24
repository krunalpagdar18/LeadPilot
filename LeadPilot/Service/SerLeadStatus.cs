using LeadPilot.Models;
using LeadPilot.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LeadPilot.Service
{
    public class SerLeadStatus
    {
        private readonly LeadPilotDbContext _context;

        public SerLeadStatus(LeadPilotDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseViewModel<List<LeadStatus>>> GeLeadStatus()
        {
            var leadStatus = await _context.LeadStatuses.ToListAsync();
            return new ResponseViewModel<List<LeadStatus>>(leadStatus);
        }
    }
}
