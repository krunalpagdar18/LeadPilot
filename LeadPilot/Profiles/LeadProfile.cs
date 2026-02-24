using AutoMapper;
using LeadPilot.Models;
using LeadPilot.ViewModels;

namespace LeadPilot.Profiles
{
    public class LeadProfile : Profile
    {
        public LeadProfile() 
        {
            CreateMap<LeadInputViewModel, Lead>();
        }
    }
}
