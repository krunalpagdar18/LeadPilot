namespace LeadPilot.ViewModels
{
    public class LeadListViewModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string Source { get; set; }
        public string Status { get; set; }
        public string EmailId { get; set; }
        public DateOnly? AddedOn { get; set; }
    }
}
