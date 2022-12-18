using ApartmentManagement.Core.Contracts;
using Hangfire;

namespace ApartmentManagement.Infrastructure.Services
{
    public class SendMailRecurringJob
    {
        private readonly ISendMailService _sendMailService;
        private readonly IRecurringJobManager _recurringJobManager;
        public SendMailRecurringJob(ISendMailService sendMailService, IRecurringJobManager recurringJobManager)
        {
            _sendMailService = sendMailService;
            _recurringJobManager = recurringJobManager;
        }
        public void MailRecurringJob()  
        {
            _recurringJobManager.AddOrUpdate("EmailJob-123", () => _sendMailService.SendMail(), "0/15 0 0 ? * * *");
        }
    }
}
