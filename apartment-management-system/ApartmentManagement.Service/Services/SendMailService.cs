using ApartmentManagement.Core.Services;
using ApartmentManagement.Core.Models;
using ApartmentManagement.Core.Models.Configuration;
using MailKit.Net.Smtp;
using MimeKit;


namespace ApartmentManagement.Infrastructure.Services
{
    public class SendMailService : ISendMailService
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly IApartmentCostService _apartmentCostService;
        public SendMailService(EmailConfiguration emailConfig, IApartmentCostService apartmentCostService)
        {
            _emailConfig = emailConfig;
            _apartmentCostService = apartmentCostService;
        }

        public async Task SendMail()
        {
            var emailToList = await _apartmentCostService.GetAllEmailByUnpaidApartmentCosts();
            EmailMessage emailMessage = new EmailMessage(emailToList, "Ödenmemiş Borç", "Ödenmemiş borcunuz bulunmaktadır.");
            var mimeMessage = CreateMimeMessage(emailMessage);
            Send(mimeMessage);
        }

        private MimeMessage CreateMimeMessage(EmailMessage emailMessage)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
            mimeMessage.To.AddRange(emailMessage.ToEmailList);
            mimeMessage.Subject = emailMessage.Subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = emailMessage.Content };

            return mimeMessage;
        }

        private void Send(MimeMessage mimeMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.Username, _emailConfig.Password);
                    client.Send(mimeMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
