using System.Threading.Tasks;
using DogsWeb.API.Email;
using DogsWeb.API.Helpers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace DogsWeb.API.Services
{
  public class SendGridEmailSender : IEmailSender
    {
    

        public async Task<SendEmailResponse> SendEmailAsync(string userEmail, string emailSubject, string message) 
        {
            
            var apiKey = "";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("", "DogsWeb");
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
