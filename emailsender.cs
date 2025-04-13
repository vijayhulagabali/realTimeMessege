using SendGrid;
using SendGrid.Helpers.Mail;

namespace ChatApp.Services
{
    public class EmailSender
    {
        private readonly string apiKey = "YOUR_SENDGRID_API_KEY";

        public async Task SendEmail(string toEmail, string subject, string message)
        {
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("noreply@chatapp.com", "Chat App");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
            await client.SendEmailAsync(msg);
        }
    }
}
