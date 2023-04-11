namespace Consumer.Domain.SeedWork
{
    public interface IExecutionStrategy
    {
        Task ExecuteAsync(Func<Task> operation);
    }
}
