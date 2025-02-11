using CasaDanaAPI.Infrastructure.Configuration;
using MailKit.Net.Smtp;
using MimeKit;
using CasaDanaAPI.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace CasaDanaAPI.Services
{
    public class MailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public MailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string htmlMessage)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_emailSettings.SenderEmail);
            email.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = htmlMessage
            };
            email.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync(_emailSettings.Host, _emailSettings.Port, false);

                if (_emailSettings.EnableSsl)
                {
                    smtp.AuthenticationMechanisms.Remove("XOAUTH2");
                }

                await smtp.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword);

                await smtp.SendAsync(email);
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }
    }
}