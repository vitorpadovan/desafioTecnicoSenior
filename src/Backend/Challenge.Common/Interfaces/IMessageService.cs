namespace Challenge.Common.Interfaces
{
    public interface IMessageService
    {
        Task PublishAsync<T>(T v, string queue = "default", string routingKey = "default", string? exchange = null);
        Task InitQueueAsync(string queue = "default", string? exchange = null, string routingKey = "");
    }
}
