using Consumer.Domain.Aggregates.OrganizationAggregate;
using Consumer.Domain.Aggregates.UserAggregate;
using Consumer.Domain.SeedWork;
using Consumer.Infastructure.DataBase.Configurations;
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
        }

        public DbSet<Organization> Organizations => Set<Organization>();
        public DbSet<User> Users => Set<User>();

        private IDbContextTransaction? _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrganizationConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        public async Task<IDbContextTransaction?> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                RollbackTransaction();
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

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.RollbackAsync();
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

        public async Task SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);
            await SaveChangesAsync(cancellationToken);
        }
    }
}