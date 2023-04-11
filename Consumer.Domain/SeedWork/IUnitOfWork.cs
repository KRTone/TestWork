using Consumer.Domain.Aggregates.OrganizationAggregate;
using Consumer.Domain.Aggregates.UserAggregate;

namespace Consumer.Domain.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
        bool HasActiveTransaction { get; }

        IExecutionStrategy CreateExecutionStrategy();
        Task SaveEntitiesAsync(CancellationToken cancellationToken = default);
        Task<Guid?> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(Guid? transactionId, CancellationToken cancellationToken = default);

        IUserRepository UserRepository { get; }
        IOrganizationRepository OrganizationRepository { get; }
    }
}
