using LeadPilot.Enum;
using LeadPilot.Models;
using LeadPilot.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LeadPilot.Service
{
    public class SerEmail
    {
        private readonly LeadPilotDbContext _context;
        private readonly SerN8n _n8nClient;
        private readonly SerLead _serLead;
        private readonly IConfiguration _config;
        public SerEmail(LeadPilotDbContext context, SerN8n n8nClient,SerLead serLead, IConfiguration config)
        {
            _context = context;
            _n8nClient = n8nClient;
            _serLead = serLead;
            _config = config;
        }

        private async Task<bool> SendEmail(string subject,string body,string ToEmail)
        {
            var fromEmail = _config["Email:FromEmail"];
            var password = _config["Email:PassKey"];
            var title= _config["Title"];
            MailAddress fromAddress = new MailAddress(fromEmail, title);
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;

            NetworkCredential info = new NetworkCredential(fromEmail, password);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = info;

            MailMessage message = new MailMessage();
            message.From = fromAddress;

            message.To.Add(ToEmail);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            await smtpClient.SendMailAsync(message);
            return true;
        }

        private async Task<bool> AddLeadEmailLog(int leadID,int templateID,EmailTypeEnum emailType)
        {
            var nextFollowUpDay = Convert.ToInt32(_config["NextFollowUp"]??"5");
            var leadEmailLog = new LeadEmailLog()
            {
                LeadId = leadID,
                EmailTemplateId = templateID,
                MailDate = DateOnly.FromDateTime(DateTime.Now),
                NextEmailDate = emailType == EmailTypeEnum.Initial ? DateOnly.FromDateTime(DateTime.Now.AddDays(nextFollowUpDay)) : null
            };

            _context.LeadEmailLogs.Add(leadEmailLog);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ResponseViewModel<bool>> SendInitialEmail(Lead leadDetails)
        {
            var emailTemplate = await _context.EmailTemplates.AsNoTracking().Where(x =>x.Active && x.EmailTypeId == (int)EmailTypeEnum.Initial && (x.SouceId == leadDetails.SourceId || x.SouceId==null)).OrderByDescending(x=>x.SouceId).FirstOrDefaultAsync();

            var subject = emailTemplate.Subject;
            var body = emailTemplate.Body;

            List<KeyValuePair<string, string>> keyWords = new List<KeyValuePair<string, string>>();
            keyWords.Add(new KeyValuePair<string, string>("@ContactName", !string.IsNullOrEmpty(leadDetails.ContactName) ? " " + leadDetails.ContactName : ""));
            keyWords.Add(new KeyValuePair<string, string>("@FirmName", leadDetails.CompanyName));
            keyWords.Add(new KeyValuePair<string, string>("@City", leadDetails.City));

            subject = keyWords.Aggregate(subject, (current, keyval) => current.Replace(keyval.Key, keyval.Value));
            body = keyWords.Aggregate(body, (current, keyval) => current.Replace(keyval.Key, keyval.Value));

            var sendEmail = await SendEmail(subject, body, leadDetails.EmailId);
            if (sendEmail)
            {
                leadDetails.StatusId = (int)LeadStatusEnum.InitialSent;
                await _serLead.UpdateLead(leadDetails);
                var logEmail = await AddLeadEmailLog(leadDetails.Id, emailTemplate.Id, (EmailTypeEnum)emailTemplate.EmailTypeId);

                await _n8nClient.NotifyLeadCreation(new
                {
                    leadDetails.Id
                });
                return new ResponseViewModel<bool>(logEmail);
            }
            else
            {
                throw new Exception("Something went wrong");
            }

        }

        public async Task<ResponseViewModel<bool>> SendFollowUpEmail(int LeadID)
        {
            var emailTemplate = await _context.EmailTemplates.AsNoTracking().Where(x => x.EmailTypeId == (int)EmailTypeEnum.Followup).FirstOrDefaultAsync();

            var leadDetails = await _context.Leads.AsNoTracking().Where(x => x.Id == LeadID && x.StatusId == (int)LeadStatusEnum.InitialSent).FirstOrDefaultAsync();
            if (leadDetails == null)
            {
                throw new Exception("Lead not found");
            }
            var subject = emailTemplate.Subject;
            var body = emailTemplate.Body;

            List<KeyValuePair<string, string>> keyWords = new List<KeyValuePair<string, string>>();
            keyWords.Add(new KeyValuePair<string, string>("@ContactName", !string.IsNullOrEmpty(leadDetails.ContactName) ? " " + leadDetails.ContactName : ""));
            keyWords.Add(new KeyValuePair<string, string>("@FirmName", leadDetails.CompanyName));
            keyWords.Add(new KeyValuePair<string, string>("@City", leadDetails.City));

            subject = keyWords.Aggregate(subject, (current, keyval) => current.Replace(keyval.Key, keyval.Value));
            body = keyWords.Aggregate(body, (current, keyval) => current.Replace(keyval.Key, keyval.Value));

            var sendEmail = await SendEmail(subject, body, leadDetails.EmailId);
            if (sendEmail)
            {
                leadDetails.StatusId = (int)LeadStatusEnum.FollowUpSent;
                await _serLead.UpdateLead(leadDetails);

                var logEmail = await AddLeadEmailLog(leadDetails.Id, emailTemplate.Id, (EmailTypeEnum)emailTemplate.EmailTypeId);
                return new ResponseViewModel<bool>(logEmail);
            }
            else
            {
                throw new Exception("Something went wrong");
            }
        }
    }
}
