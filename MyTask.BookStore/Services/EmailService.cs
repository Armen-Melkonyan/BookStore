using Microsoft.Extensions.Options;
using MyTask.BookStore.Models;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MyTask.BookStore.Services
{
    public class EmailService : IEmailService
    {
        private const string templatePath = @"EmailTemplate/{0}.html";
        private readonly SMTPConfigModel _smtpConfig;

        public async Task SendtestEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = "Sending for test";
            userEmailOptions.Body = GetEmailBody("TestEmail");

            await SendEmail(userEmailOptions);
        }

        public EmailService(IOptions<SMTPConfigModel> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }

        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage()
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayAddress),
                IsBodyHtml = _smtpConfig.IsBodyHTML
            };

            foreach (var toEmail in userEmailOptions.ToEmail)
            {
                mail.To.Add(toEmail);
            }

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.password);

            SmtpClient smtp = new SmtpClient()
            {
                Host = _smtpConfig.host,
                Port = _smtpConfig.port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredientials,
                Credentials = networkCredential
            };
            mail.BodyEncoding = default;

            await smtp.SendMailAsync(mail);
        }

        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatePath, templateName));
            return body;
        }
    }
}
