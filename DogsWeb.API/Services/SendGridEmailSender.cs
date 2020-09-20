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
            
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("raqbiel@gmail.com", "DogsWeb");
            var subject = emailSubject;
            var to = new EmailAddress(userEmail, "Test");
            var plainTextContent = message;
            var htmlContent = message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return new SendEmailResponse();
        }
        // public async Task<SendEmailResponse> SendEmailAsyncs(string userEmail, string emailSubject, string message){

        //     var sendGridClient = new SendGridClient("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");
        //     var sendGridMessage = new SendGridMessage();
        //     sendGridMessage.SetFrom("vfabing@live.com", "Vivien FABING");
        //     sendGridMessage.AddTo("usertest2019@yopmail.com", "user test");
        //     sendGridMessage.SetTemplateId("MY_TEMPLATE_ID");
        //     sendGridMessage.SetTemplateData(new HelloEmail
        //     {
        //         Name = "Vivien",
        //         Url = "https://www.vivienfabing.com"
        //     });

        //     var response = sendGridClient.SendEmailAsync(sendGridMessage).Result;
        //     if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
        //     {
        //         Console.WriteLine("Email sent");
        //     }

        //      return new SendEmailResponse();
        // }
        //     private class HelloEmail
        // {
        //     [JsonProperty("name")]
        //     public string Name { get; set; }

        //     [JsonProperty("url")]
        //     public string Url { get; set; }
        // }

        
    }
}