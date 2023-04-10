namespace Producer.Domain.Interfaces
{
    public interface IPublisher<T> where T : class
    {
        Task PublishAsync(T value, CancellationToken token = default);
    }
}