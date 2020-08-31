using DogsWeb.API.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DogsWeb.API.Email
{
    public static class SendGridExtensions
    {
        public static IServiceCollection AddSendGridEmailSender(this IServiceCollection services) 
        {
            services.AddTransient<IEmailSender, SendGridEmailSender>();

            return services;
         }
    }
}