namespace Core.Application.Services
{
    public interface IEventProducer
    {
        Task ProduceEvent(string topic, string message);
    }
}
