using AutoMapper;
using LeadPilot.Models;
using LeadPilot.Profiles;
using LeadPilot.Service;
using LeadPilot.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace LeadPilot.Controllers
{
    public class LeadController : Controller
    {
        private readonly SerLead _serLead;
        private readonly SerLeadSource _serLeadSource;
        private readonly SerLeadStatus _serLeadStatus;
        private readonly SerEmail _serEmail;
        private readonly IMapper _mapper;
        private readonly string secret;
        private readonly IConfiguration _config;

        public LeadController(SerLead serLead, SerLeadSource serLeadSource, SerLeadStatus serLeadStatus, SerEmail serEmail, IMapper mapper, IConfiguration config)
        {
            _serLead = serLead;
            _serLeadSource = serLeadSource;
            _serLeadStatus = serLeadStatus;
            _serEmail = serEmail;
            _mapper = mapper;
            _config = config;
            secret = _config["N8N:WebhookSecret"];
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateLead()
        {
            var leadVM=new LeadViewModel();
            var leadSource = await _serLeadSource.GetLeadSource();
            var leadStatus = await _serLeadStatus.GeLeadStatus();

            leadVM.LeadSource = leadSource.responseData;
            leadVM.LeadStatus = leadStatus.responseData;
            return View("AddUpdate", leadVM);
        }

        public async Task<IActionResult> Open(int id)
        {
            var leadVm=new LeadViewModel();
            var lead = await _serLead.GetLeadByID(id);
            leadVm.Lead = lead.responseData;

            var leadSource = await _serLeadSource.GetLeadSource();
            var leadStatus = await _serLeadStatus.GeLeadStatus();

            leadVm.LeadSource = leadSource.responseData;
            leadVm.LeadStatus = leadStatus.responseData;
            return View("AddUpdate", leadVm);
        }

        [HttpPost]
        public async Task<IActionResult> AddUpdateLead([FromBody] LeadInputViewModel leadVM)
        {
            if (leadVM == null)
            {
                return Json(new {success=false,message="Invalid data"});
            }

            var lead = _mapper.Map<Lead>(leadVM);

            if (lead.Id == 0)
            {
                var createLead = await _serLead.CreateLead(lead);
                if (createLead.Status) 
                {
                    var emailStatus = await _serEmail.SendInitialEmail(createLead.responseData);
                    return Json(new { success = emailStatus.Status, message = emailStatus.Status ? "Lead created successfully. Email Sent" : emailStatus.ErrorMessage });
                }

                return Json(new { success = createLead.Status, message = createLead.ErrorMessage });
            }
            else 
            {
                var updateLead=await _serLead.UpdateLead(lead);
                return Json(new { success = updateLead.Status, message = updateLead.Status ? updateLead.responseData : updateLead.ErrorMessage });
            }
        }


        [HttpPost]
        public async Task<IActionResult> TriggerFollowup([FromQuery] int Id)
        {
            var IncomingSecret = Request.Headers["x-leadpilot-secret"].ToString();
            if(IncomingSecret!= secret)
            {
                return Unauthorized();
            }

            var followUpEmail= await _serEmail.SendFollowUpEmail(Id);
            return Ok();
           
        }

        [HttpPost]
        public async Task<IActionResult> GetAllLeads(PaginationViewModel paginationViewModel)
        {
            var leadData= await _serLead.GetLeads(paginationViewModel);

            return Json(leadData.responseData);
        }
    }
}
