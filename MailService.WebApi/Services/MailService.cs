using MailKit.Net.Smtp;
using MailKit.Security;
using MailService.WebApi.Models;
using MailService.WebApi.Settings;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.IO;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
namespace MailService.WebApi.Services
{
public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
     
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
          
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            string text = File.ReadAllText(@"C:\Users\thinksysuser\Desktop\Data\NewData\allfiles.txt");
            var builder = new BodyBuilder();
            builder.TextBody = text; ;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

       
    }
}
