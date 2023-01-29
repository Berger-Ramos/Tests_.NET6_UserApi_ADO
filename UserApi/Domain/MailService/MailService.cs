using Microsoft.Extensions.Diagnostics.HealthChecks;
using Library.MessageFrameWork.RabbitMQ;

namespace UserApi.Domain.MailService
{
    public class MailService
    {
        public IPublisher publisher { get; set; }

        public MailService()
        {
            publisher = new Publisher(hostName: "localhost", queue: "MailQueue");
        }

        public void GenerateMailQueue(string message)
        {
            publisher.GenerateMessage(message);
        }
    }
}
