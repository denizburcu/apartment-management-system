using MimeKit;

namespace ApartmentManagement.Core.Models
{
    public class EmailMessage
    {
        public List<MailboxAddress> ToEmailList { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public EmailMessage(IEnumerable<string> to, string subject, string content)
        {
            ToEmailList = new List<MailboxAddress>();
            ToEmailList.AddRange(to.Select(email => new MailboxAddress("email", email)));
            Subject = subject;
            Content = content;
        }
    }
}
