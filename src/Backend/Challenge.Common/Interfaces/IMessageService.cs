namespace Challenge.Common.Interfaces
{
    public interface IMessageService
    {
        Task PublishAsync<T>(T v, string queue = "default", string routingKey = "default", bool forceRoutingKey = false);
        Task InitQueueAsync(string queue = "default");
    }
}
