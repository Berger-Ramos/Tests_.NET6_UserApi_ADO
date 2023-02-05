using Microsoft.Extensions.Diagnostics.HealthChecks;
using Library.MessageFrameWork.RabbitMQ;
using Library.Entity;
using System.Text.Json;
using MailConsumer.Domain;

namespace UserApi.Domain.MailService
{
    public class MailService
    {
        public IPublisher publisher { get; set; }

        private MailQueue MailQueue { get; set; }

        public MailService()
        {
            publisher = new Publisher(hostName: "localhost", queue: "MailQueue");
        }

        public void GenerateMailQueue()
        {
            string message = JsonSerializer.Serialize(MailQueue);
            publisher.GenerateMessage(message);
        }

        public void FormatMailToSend(string toMail, string name)
        {
            string contentMail = string.Format("Hello {0}, now you have a user, for User.API", name);

            MailQueue = new MailQueue()
            {
                ToMailAddress = toMail,
                Content = contentMail
            };
        }
    }
}
