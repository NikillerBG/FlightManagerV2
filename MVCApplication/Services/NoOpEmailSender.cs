using Microsoft.AspNetCore.Identity.UI.Services;

namespace MVCApplication.Services
{
    public class NoOpEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Do nothing — no-op
            return Task.CompletedTask;
        }
    }
}
