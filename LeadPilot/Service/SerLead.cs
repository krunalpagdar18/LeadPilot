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
            lead.Inactive = false;
            var newLead = _context.Leads.Add(lead);
            await _context.SaveChangesAsync();

            return new ResponseViewModel<Lead>(newLead.Entity);
        }

        public async Task<ResponseViewModel<string>> UpdateLead(Lead lead)
        {
            lead.Inactive = false;
            _context.Entry(lead).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new ResponseViewModel<string>("Lead updated");
        }

        public async Task<ResponseViewModel<bool>> DeleteLead(int ID)
        {
           var lead= await _context.Leads.Where(x => x.Id == ID && x.Inactive == false).FirstOrDefaultAsync();
            if (lead == null)
            {
                throw new Exception("Lead not found");
            }


            lead.Inactive = true;
            lead.InactiveDate= DateTime.Now;

            await _context.SaveChangesAsync();

            return new ResponseViewModel<bool>(true);
        }

        public async Task<ResponseViewModel<Lead>> GetLeadByID(int ID)
        {
            var lead = await _context.Leads.AsNoTracking().Where(x => x.Id == ID && x.Inactive==false).FirstOrDefaultAsync();
            if (lead == null)
            {
                throw new Exception("Lead not found");
            }
            return new ResponseViewModel<Lead>(lead);
        }

        public async Task<ResponseViewModel<ListViewModel<List<LeadListViewModel>>>> GetLeads(PaginationViewModel LeadPageVM)
        {
            var query = _context.Leads.Include(x => x.Source).Include(x => x.Status).AsNoTracking()
                                    .Where(x => x.Inactive == false && x.StatusId!=(int)LeadStatusEnum.NotInterested);
            var totalCount = await query.CountAsync();
            if (!string.IsNullOrEmpty(LeadPageVM.SearchText))
            {
                var search = $"%{LeadPageVM.SearchText}%";

                query = query.Where(x=>EF.Functions.ILike(x.CompanyName,search)
                                || EF.Functions.ILike(x.City, search)
                                || EF.Functions.ILike(x.EmailId, search)
                                || EF.Functions.ILike(x.Source.Name, search)
                                || EF.Functions.ILike(x.Status.Name, search)
                            );
            }

            var leadListdata = await query
                                    .OrderByDescending(x=>x.AddedOn)
                                    .Skip((LeadPageVM.PageIndex - 1) * LeadPageVM.PageSize).Take(LeadPageVM.PageSize)
                                    .Select(x => new LeadListViewModel()
                                    {
                                        Id = x.Id,
                                        CompanyName = x.CompanyName,
                                        City = x.City,
                                        EmailId = x.EmailId,
                                        Source = x.Source.Name,
                                        Status = x.Status.Name,
                                        AddedOn=x.AddedOn
                                    }).ToListAsync();

            var filteredCount = await query.CountAsync();

            var lstData = new ListViewModel<List<LeadListViewModel>>()
            {
                data = leadListdata,
                recordsTotal = totalCount,
                recordsFiltered = filteredCount
            };

            return new ResponseViewModel<ListViewModel<List<LeadListViewModel>>>(lstData);
        }

        public async Task<ResponseViewModel<string>> GetLeadStatusByID(int leadID)
        {
            var leadStatus=await _context.Leads.AsNoTracking().Include(x=>x.Status).Where(x=>x.Id == leadID && x.Inactive==false).Select(x=>x.Status.Name).FirstOrDefaultAsync();
            if (string.IsNullOrEmpty(leadStatus))
            {
                throw new Exception("Lead not found");
            }

            return new ResponseViewModel<string>(leadStatus);
        }
    }
}
