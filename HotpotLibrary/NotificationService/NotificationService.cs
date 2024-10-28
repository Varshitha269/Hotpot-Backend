using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotpotLibrary.NotificationService
{
    public class NotificationService
    {
        private readonly IEmailService _emailService;

        public NotificationService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public void SendNotification(string to, string subject, string message)
        {
            _emailService.SendEmail(to, subject, message);
        }
    }
}
