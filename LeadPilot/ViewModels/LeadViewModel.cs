using LeadPilot.Models;

namespace LeadPilot.ViewModels
{
    public class LeadViewModel
    {
        public Lead Lead { get; set; }
        public List<LeadSource> LeadSource { get; set; }
        public List<LeadStatus> LeadStatus { get; set; }
    }
}
