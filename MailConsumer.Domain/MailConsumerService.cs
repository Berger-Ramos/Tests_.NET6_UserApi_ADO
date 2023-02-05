using System.Net.Mail;
using System.Net;
using System.Text.Json;

namespace MailConsumer.Domain
{
    public class MailConsumerService : IServiceConsumer
    {
        private static string UserName { get; set; }

        private static string Password { get; set; }

        public MailConsumerService(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public void SendMessage(string message)
        {
            MailQueue mailQueue = JsonSerializer.Deserialize<MailQueue>(message);

            Task task = ExecuteSendEmailAsync(mailQueue);

            task.Wait();

        }

        private static async Task ExecuteSendEmailAsync(MailQueue mailQueue)
        {
            MailMessage mail = new MailMessage
            {
                From = new MailAddress(UserName, "EmailService")
            };

            mail.To.Add(new MailAddress(mailQueue.ToMailAddress));
            //mail.CC.Add(new MailAddress(_emailSettings.CcEmail));

            mail.Subject = "EmailService .net - ";
            mail.Body = mailQueue.Content;
            mail.IsBodyHtml = false;
            mail.Priority = MailPriority.Normal;

            using (SmtpClient smtp = new SmtpClient("smtp.office365.com", 587))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(UserName, Password);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }
        }
    }
}