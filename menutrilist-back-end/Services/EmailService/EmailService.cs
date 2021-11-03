using MailKit.Net.Smtp;
using MailKit.Security;
using Menutrilist.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Threading.Tasks;

namespace Menutrilist.Services
{


    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger, IOptions<EmailSettings> emailSettings)
        {
            _logger = logger;
            _emailSettings = emailSettings.Value;
        }

        private SecureSocketOptions SecureOptions(string options)
        {
            if (!String.IsNullOrEmpty(options))
            {
                string sOptions = options.ToUpper();
                if (sOptions == "NONE")
                    return SecureSocketOptions.None;
                if (sOptions == "SSLONCONNECT")
                    return SecureSocketOptions.SslOnConnect;
                if (sOptions == "STARTTLS")
                    return SecureSocketOptions.StartTls;
                if (sOptions == "STARTTLSWHENAVAILABLE")
                    return SecureSocketOptions.StartTlsWhenAvailable;
            }

            return SecureSocketOptions.Auto;
        }

        public async Task<bool> SendAsync(string to, string subject, string html, string from = null)
        {
            // create message
            int retryCount = _emailSettings.RetryCount;
            if (retryCount <= 0)
                retryCount = 1;

            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(from ?? _emailSettings.EmailFrom),
                Subject = subject,
                Body = new TextPart(TextFormat.Html) { Text = html }
            };
            email.To.Add(MailboxAddress.Parse(to));

            // send email
            for (var count = 1; count <= retryCount; count++)
            {
                try
                {
                    using var smtp = new SmtpClient();
                    await smtp.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureOptions(_emailSettings.SecureSocketOptions));
                    if (_emailSettings.SecureSocketOptions != "NONE")
                    {
                        smtp.AuthenticationMechanisms.Remove("XOAUTH2");
                        await smtp.AuthenticateAsync(_emailSettings.EmailUser, _emailSettings.EmailPassword);
                    }
                    await smtp.SendAsync(email);
                    await smtp.DisconnectAsync(true);

                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(0, ex, "MailKit.Send failed attempt {0}", count);
                    await Task.Delay(count * 1000);
                }
            }
            return false;
        }
    }
}
