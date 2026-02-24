using LeadPilot.Enum;
using LeadPilot.Models;
using LeadPilot.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LeadPilot.Service
{
    public class SerLead
    {
        private readonly LeadPilotDbContext _context;
        public SerLead(LeadPilotDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseViewModel<Lead>> CreateLead(Lead lead)
        {
            lead.StatusId = (int?)LeadStatusEnum.New;
            lead.AddedOn = DateOnly.FromDateTime(DateTime.Now);
            var newLead = _context.Leads.Add(lead);
            await _context.SaveChangesAsync();

            return new ResponseViewModel<Lead>(newLead.Entity);
        }

        public async Task<ResponseViewModel<string>> UpdateLead(Lead lead)
        {
            _context.Entry(lead).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new ResponseViewModel<string>("Lead updated");
        }

        public async Task<ResponseViewModel<Lead>> GetLeadByID(int ID)
        {
            var lead = await _context.Leads.Where(x => x.Id == ID).FirstOrDefaultAsync();
            if (lead == null)
            {
                throw new Exception("Lead not found");
            }
            return new ResponseViewModel<Lead>(lead);
        }

        public async Task<ResponseViewModel<ListViewModel<List<LeadListViewModel>>>> GetLeads(PaginationViewModel LeadPageVM)
        {
            var leadListdata = await _context.Leads.Include(x => x.Source).Include(x => x.Status).AsNoTracking()
                                    .Skip((LeadPageVM.PageIndex - 1) * LeadPageVM.PageSize).Take(LeadPageVM.PageSize)
                                    .Select(x => new LeadListViewModel()
                                    {
                                        Id = x.Id,
                                        CompanyName = x.CompanyName,
                                        City = x.City,
                                        EmailId = x.EmailId,
                                        Source = x.Source.Name,
                                        Status = x.Status.Name
                                    }).ToListAsync();

            var totalCount = await _context.Leads.Include(x => x.Source).Include(x => x.Status).AsNoTracking().CountAsync();

            var lstData = new ListViewModel<List<LeadListViewModel>>()
            {
                data = leadListdata,
                recordsTotal = totalCount,
                recordsFiltered = totalCount
            };

            return new ResponseViewModel<ListViewModel<List<LeadListViewModel>>>(lstData);
        }
    }
}
