namespace MailConsumer.Domain
{
    public interface IServiceConsumer
    {
        void SendMessage(string message);
    }
}
