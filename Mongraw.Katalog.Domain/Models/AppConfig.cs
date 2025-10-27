using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongraw.Katalog.Domain.Models
{
    public class AppConfig
    {
        public EmailSettings Email { get; set; }
    }

    public class EmailSettings
    {
        public string EmailAddress { get; set; }
        public int Port { get; set; }
        public string SMTP { get; set; }
        public string SenderEmail { get; set; }
        public string Username { get; set; }
        public string EmailPassword { get; set; }
        public string MailTo { get; set; }
    }
}
