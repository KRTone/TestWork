namespace Consumer.Domain.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        T Add(T entity);
        Task<T?> GetAsync(Guid guid, CancellationToken token = default);
    }
}
