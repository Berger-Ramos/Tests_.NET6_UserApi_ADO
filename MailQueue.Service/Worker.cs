using Library.MessageFrameWork.RabbitMQ;
using MailConsumer.Domain;
using Microsoft.Extensions.Logging;

namespace MailQueue.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);


                await Task.Delay(1000, stoppingToken);

                try
                {
                    IConsumer consumerMessage = new Consumer(hosname: "localhost", queue: "MailQueue", logger : _logger);

                    consumerMessage.GetMessage();

                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                }
                

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}