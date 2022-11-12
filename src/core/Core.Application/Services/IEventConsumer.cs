namespace Core.Application.Services
{
    public interface IEventConsumer
    {
        Task ConsumeEvent(string topic, Func<string, Task> callback, CancellationToken cancellationToken);
    }
}
