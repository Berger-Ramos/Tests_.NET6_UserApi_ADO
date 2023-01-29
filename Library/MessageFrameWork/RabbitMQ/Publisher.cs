using Library.Entity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RabbitMQ.Client;
using System.Text;

namespace Library.MessageFrameWork.RabbitMQ
{
    public class Publisher : IPublisher
    {
        public Publisher(string hostName, string queue)
        {
            HostName = hostName;
            Queue = queue;
        }

        public string Queue { get; set; }

        public string HostName { get; set; }

        public void GenerateMessage(string message)
        {
            var factory = new ConnectionFactory()
            {

                Uri = new Uri(@"amqp://guest:guest@localhost:5672/")
                //HostName = HostName,
                //Port = 5672
                //UserName = "guest",
                //Password = "guest",
                //ContinuationTimeout = TimeSpan.MaxValue

            };


            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: Queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: Queue,
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}
