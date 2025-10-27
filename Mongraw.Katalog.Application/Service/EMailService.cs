using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using Mongraw.Katalog.Application.Service.Interfaces;
using Mongraw.Katalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Mongraw.Katalog.Application.Service
{
    public class EMailService : IEMailService
    {
        private readonly AppConfig _options;

        public EMailService(IOptions<AppConfig> options)
        {
            _options = options.Value;
        }

        public void SendEmail(Email email)
        {

            var builder = new BodyBuilder();
            builder.HtmlBody = email.Body;
            MimeMessage message = new MimeMessage();

            message.From.Add(new MailboxAddress(email.From, _options.Email.EmailAddress));
            message.To.Add(MailboxAddress.Parse(email.To));
            message.Subject = email.Subject;

            message.Body = builder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            try
            {
                client.Connect(_options.Email.SMTP, _options.Email.Port, true);
                client.Authenticate(_options.Email.EmailAddress, _options.Email.EmailPassword);
                client.Send(message);
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                client.Disconnect(true);
            }
        }
    }
}
