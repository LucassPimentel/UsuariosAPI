using MailKit.Net.Smtp;
using MimeKit;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class EmailService
    {

        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmailWithActivateCode(string[] emailAddressees, string subject, int userId, string activateCode)
        {
            Message message = new Message(emailAddressees, subject, userId, activateCode);

            var emailMessage = CreateEmailBody(message);
            SendEmail(emailMessage);
        }


        private void SendEmail(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    // conecta ao servidor de email 
                    client.Connect(_configuration.GetValue<string>("EmailSettings:SmtpServer"), _configuration.GetValue<int>("EmailSettings:Port"), true);

                    client.AuthenticationMechanisms.Remove("XOUATH2");

                    client.Authenticate(_configuration.GetValue<string>("EmailSettings:From"), _configuration.GetValue<string>("EmailSettings:Password"));

                    client.Send(emailMessage);
                }
                catch (Exception)
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

        private MimeMessage CreateEmailBody(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_configuration.GetValue<string>("EmailSettings:From")));
            emailMessage.To.AddRange(message.EmailAddresses);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = message.Content
            };

            return emailMessage;
        }
    }
}
