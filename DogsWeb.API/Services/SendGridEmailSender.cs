using System.Threading.Tasks;
using DogsWeb.API.Email;
using DogsWeb.API.Helpers;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace DogsWeb.API.Services
{
  public class SendGridEmailSender : IEmailSender
    {
        private readonly AppSettings _appSettings;

        public SendGridEmailSender(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }


        public async Task<SendEmailResponse> SendEmailAsync(string userEmail, string emailSubject, string message) 
        {
            
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("raqbiel@gmail.com", "TECHHOWDY.COM");
            var subject = emailSubject;
            var to = new EmailAddress(userEmail, "Test");
            var plainTextContent = message;
            var htmlContent = message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return new SendEmailResponse();
        }
    }
}