using MailConsumer.Domain;

namespace MailQueue.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public readonly IConsumer consumerMessage;

        public Worker(ILogger<Worker> logger, IConsumer consumer)
        {
            _logger = logger;
            consumerMessage = consumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);


                await Task.Delay(1000, stoppingToken);

                try
                {
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