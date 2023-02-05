using MailConsumer.Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Library.MessageFrameWork.RabbitMQ
{
    public class Consumer : IConsumer
    {
        public IServiceConsumer ServiceConsumer { get; set; }
        private string  Queue { get; set; }

        private string Hostname { get; set; }

        public Consumer(string queue, string hosname, IServiceConsumer serviceConsumer)
        {
            Queue = queue;
            Hostname = hosname;
           
            ServiceConsumer = serviceConsumer;
        }

        public string GetMessage()
        {
            var factory = new ConnectionFactory() {
                //Uri = new Uri(@"amqp://WokerService:WokerService@rabbitserver:5672/"),
                HostName = Hostname,
                UserName = "guest",
                Password = "guest",
                Port = 5672
               
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: Queue,
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

                    ServiceConsumer.SendMessage(message);

                    
                };
                channel.BasicConsume(queue: Queue,
                                     autoAck: false,
                                     consumer: consumer);

            }

            return null;
        }
    }
}
