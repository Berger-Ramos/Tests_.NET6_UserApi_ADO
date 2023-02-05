using Library.MessageFrameWork.RabbitMQ;
using MailConsumer.Domain;
using MailQueue.Service;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        

        services.AddSingleton<IConsumer, Consumer>(c =>
        {
            IConfiguration config = c.GetRequiredService<IConfiguration>();

            string _hostname = config["RabbitConfig:HostName"];
            string queueName = config["RabbitConfig:Queue"];

            string mailUserName = config["MailServiceConfig:UserName"];
            string mailPassword = config["MailServiceConfig:Password"];

            return new Consumer(
                hosname: _hostname,
                queue: queueName,
                serviceConsumer: new MailConsumerService(userName : mailUserName, password : mailPassword)
                );
        });
    })
    .Build();

await host.RunAsync();
