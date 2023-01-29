// See https://aka.ms/new-console-template for more information
using Library.MessageFrameWork.RabbitMQ;
using MailConsumer.Domain;

IConsumer consumerMessage= new Consumer(hosname: "localhost", queue: "MailQueue");

consumerMessage.GetMessage();


Thread.Sleep(1000);