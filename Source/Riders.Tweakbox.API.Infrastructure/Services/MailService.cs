using Microsoft.Extensions.Logging;
using Riders.Tweakbox.API.Application.Models.Config;
using Riders.Tweakbox.API.Application.Services;
using Scriban;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Riders.Tweakbox.API.Infrastructure.Services
{
    public class MailService : IMailService, IDisposable
    {
        private static readonly string MailTemplatePath     = $"{Path.GetDirectoryName(typeof(GeoIpService).Assembly.Location)}/Assets/Templates";
        private static readonly string MailRegisterTemplate = File.ReadAllText($"{MailTemplatePath}/RegistrationTemplate.html");
        private static readonly string MailResetTemplate = File.ReadAllText($"{MailTemplatePath}/PasswordReset.html");

        private MailSettings _settings;
        private ILogger<MailService> _logger;
        private SmtpClient _client;

        public MailService(MailSettings settings, ILogger<MailService> logger)
        {
            _settings = settings;
            _logger = logger;
            _client = new SmtpClient(settings.Host)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(settings.Username, settings.Password),
                Port = settings.Port,
                EnableSsl = true,
            };
        }

        public void Dispose() => _client?.Dispose();

        public async Task SendConfirmationEmail(string email, string userName)
        {
            if (!IsMailConfigured())
                return;

            var template = Template.Parse(MailRegisterTemplate);
            var result   = template.Render(new { Name = userName });
            await SendMail(email, "Riders.Tweakbox API Registration", result);
        }

        public async Task SendPasswordResetToken(string email, string userName, string token)
        {
            if (!IsMailConfigured())
                return;
        
            var template = Template.Parse(MailResetTemplate);
            var result   = template.Render(new { Name = userName, Code = token });
            await SendMail(email, "Riders.Tweakbox API Password Reset", result);
        }

        private async Task SendMail(string to, string subject, string body)
        {
            var mail = new MailMessage();
            mail.From = new MailAddress(_settings.Username);
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            await _client.SendMailAsync(mail);
        }

        private bool IsMailConfigured()
        {
            if (string.IsNullOrEmpty(_settings.Host) || string.IsNullOrEmpty(_settings.Username) || string.IsNullOrEmpty(_settings.Password))
            {
                _logger.LogWarning("Email is Not Configured! Email will not send!!");
                return false;
            }

            return true;
        }
    }
}
