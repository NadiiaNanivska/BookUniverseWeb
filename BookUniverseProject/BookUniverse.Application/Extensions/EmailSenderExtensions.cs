using BookUniverse.Infrastructure.Services.EmailSender;
using System.Text.Encodings.Web;

namespace BookUniverse.Application.Extensions
{
    public static class EmailSenderExtensions
    {
        public static Task<bool> SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
