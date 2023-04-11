using Consumer.Domain.Aggregates.OrganizationAggregate;
using Consumer.Domain.Aggregates.UserAggregate;
using Consumer.Domain.SeedWork;
using Consumer.Infastructure.DataBase.Configurations;
using Consumer.Infastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Consumer.Infastructure.DataBase
{
    public class ConsumerDbContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "consumer";
        private readonly IMediator _mediator;

        public ConsumerDbContext(DbContextOptions<ConsumerDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
            UserRepository = new UserRepository(this);
            OrganizationRepository = new OrganizationRepository(this);
        }

        public DbSet<Organization> Organizations => Set<Organization>();
        public DbSet<User> Users => Set<User>();

        private IDbContextTransaction? _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;

        public IRepository<User> UserRepository { get; }
        public IRepository<Organization> OrganizationRepository { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrganizationConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        public async Task<Guid?> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

            return _currentTransaction.TransactionId;
        }

        public async Task CommitTransactionAsync(Guid? transactionId, CancellationToken cancellationToken = default)
        {
            if (transactionId == null) throw new ArgumentNullException(nameof(transactionId));
            if (transactionId != _currentTransaction.TransactionId) throw new InvalidOperationException($"Transaction {transactionId} is not current");

            try
            {
                await SaveChangesAsync(cancellationToken);
                await _currentTransaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                if (_currentTransaction != null)
                    await _currentTransaction.RollbackAsync();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        public async Task SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);
            await SaveChangesAsync(cancellationToken);
        }

        public Domain.SeedWork.IExecutionStrategy CreateExecutionStrategy()
        {
            return new ExecutionStrategyWrapper(Database.CreateExecutionStrategy());
        }

        public class ExecutionStrategyWrapper : Domain.SeedWork.IExecutionStrategy
        {
            private readonly Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy _internalStrategy;

            public ExecutionStrategyWrapper(Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy internalStrategy)
            {
                _internalStrategy = internalStrategy;
            }

            public Task ExecuteAsync(Func<Task> operation)
            {
                //return _internalStrategy.ExecuteInTransactionAsync(operation, verifySucceeded: () => );
                return _internalStrategy.ExecuteAsync(operation);
            }
        }
    }
}