using Microsoft.Extensions.Configuration;
using MitigatingCircumstances.Services.Interface;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MitigatingCircumstances.Services
{
    public class SendGridMailService : IMailService
    {
        private readonly SendGridClient _sendGridClient;

        public SendGridMailService(IConfiguration config)
        {
            var apiKey = config["Mail:SendGridApiKey"];
            _sendGridClient = new SendGridClient(apiKey);
        }

        public void SendEmail(string toEmail, string toName)
        {
            var from = new EmailAddress("no-reply@mail.supporthub.cloud", "Support Hub");
            var subject = "Ladies and gentlemen, we've got em";
            var to = new EmailAddress(toEmail, toName);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = _sendGridClient.SendEmailAsync(msg).Result;
        }

        public void SendEmail(string toEmail, string toName, string message)
        {
            var from = new EmailAddress("no-reply@mail.supporthub.cloud", "Support Hub");
            var subject = "Ladies and gentlemen, we've got em";
            var to = new EmailAddress(toEmail, toName);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, string.Empty); 
            var response = _sendGridClient.SendEmailAsync(msg).Result;
        }
    }
}
