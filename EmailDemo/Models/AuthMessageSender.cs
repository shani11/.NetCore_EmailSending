using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmailDemo.Models
{
    public class AuthMessageSender : IEmailSender
    {
        public EmailSettings _emailSettings;
        public AuthMessageSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public Task SendEmailAsync(List<string> email, string subject, string message)
        {
            Execute(email, subject, message).Wait();
            return Task.FromResult(0);

        }


        public async Task Execute(List<string> email, string subject, string message)
        {
            try
            {
               // string toEmail = string.IsNullOrEmpty(email)
                 //                ? _emailSettings.ToEmail
                   //              : email;
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "Shani Bhati")
                };
                foreach (string item in email)
                {
                    mail.To.Add(new MailAddress(item));

                }
                // mail.CC.Add(new MailAddress(_emailSettings.CcEmail));

                mail.Subject = "Feedback mail Management System - " + subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_emailSettings.SecondayDomain, _emailSettings.SecondaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                //do something here
            }
        }

    }
}
