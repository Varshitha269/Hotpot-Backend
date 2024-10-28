using System;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using log4net;

namespace HotpotLibrary.NotificationService
{
   
    public class GmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(GmailService));

        public GmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string to, string subject, string body)
        {
            try
            {
                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
                var senderEmail = _configuration["EmailSettings:SenderEmail"];
                var senderName = _configuration["EmailSettings:SenderName"];
                var smtpUser = _configuration["EmailSettings:SmtpUser"];
                var smtpPassword = _configuration["EmailSettings:SmtpPassword"];
                var useSsl = bool.Parse(_configuration["EmailSettings:UseSsl"]);

                var fromAddress = new MailAddress(senderEmail, senderName);
                var toAddress = new MailAddress(to);

                using (var smtp = new SmtpClient
                {
                    Host = smtpServer,
                    Port = smtpPort,
                    EnableSsl = useSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(smtpUser, smtpPassword),
                    Timeout = 20000 // Timeout in milliseconds
                })
                {
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true // Important: Set this to true to send HTML email
                    })
                    {
                        smtp.Send(message);
                    }
                }

                _logger.Info($"Email sent via Gmail to {to}: {subject}");
            }
            catch (SmtpException smtpEx)
            {
                _logger.Error($"SMTP Error: {smtpEx.Message}", smtpEx);
            }
            catch (Exception ex)
            {
                _logger.Error($"Failed to send email: {ex.Message}", ex);
            }
        }
    }

}
