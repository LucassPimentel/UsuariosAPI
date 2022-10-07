using MimeKit;

namespace UsuariosAPI.Models
{
    public class Message
    {
        public List<MailboxAddress> EmailAddresses { get; set; }

        public string Subject { get; set; }
        public string Content { get; set; }

        public Message(IEnumerable<string> addresses, string subject, int userId, string activateCode)
        {
            EmailAddresses = new List<MailboxAddress>();
            EmailAddresses.AddRange(addresses.Select(d => new MailboxAddress(d)));
            Subject = subject;
            Content = $"https://localhost:7152/Email/Activation?Id={userId}&ActivateToken={activateCode}";
        }
    }
}
