using MailConsumer.Domain;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Library.MessageFrameWork.RabbitMQ
{
    public class Consumer : IConsumer
    {
        ILogger Logger;

        private string  Queue { get; set; }

        private string Hostname { get; set; }

        public Consumer(string queue, string hosname, ILogger logger)
        {
            Queue = queue;
            Hostname = hosname;
            Logger = logger;
        }

        public string GetMessage()
        {
            var factory = new ConnectionFactory() {
                //Uri = new Uri(@"amqp://WokerService:WokerService@rabbitserver:5672/"),
                HostName = "rabbitmq",
                UserName = "guest",
                Password = "guest",
                Port = 5672
               
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "MailQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);

                    Logger.LogDebug(" Received {0}", message);
                };
                channel.BasicConsume(queue: "MailQueue",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");

            }

            return null;
        }

        private void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
    }


}
