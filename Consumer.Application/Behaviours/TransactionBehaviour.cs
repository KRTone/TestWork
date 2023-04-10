using Consumer.Infastructure.DataBase;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using Utils;

namespace Consumer.Application.Behaviours
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
        private readonly ConsumerDbContext _dbContext;

        public TransactionBehaviour(ConsumerDbContext dbContext, ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response = default;
            string typeName = request.GetGenericTypeName();

            try
            {
                if (_dbContext.HasActiveTransaction)
                {
                    return await next();
                }

                IExecutionStrategy strategy = _dbContext.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;

                    await using IDbContextTransaction transaction = (await _dbContext.BeginTransactionAsync())!;
                    using (LogContext.PushProperty("TransactionContext", transaction.TransactionId))
                    {
                        _logger.LogInformation($"----- Begin transaction {transaction.TransactionId} for {typeName} ({request})");

                        response = await next();

                        _logger.LogInformation($"----- Commit transaction {transaction.TransactionId} for {typeName}");

                        await _dbContext.CommitTransactionAsync(transaction);
                    }
                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ERROR Handling transaction for {typeName} ({request})");

                throw;
            }
        }
    }
}