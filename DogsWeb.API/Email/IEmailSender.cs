using System.Threading.Tasks;
using System;

namespace DogsWeb.API.Email
{
    public interface IEmailSender
    {
          Task<SendEmailResponse> SendEmailAsync(string userEmail, string emailSubject, string message);
      
    }
}