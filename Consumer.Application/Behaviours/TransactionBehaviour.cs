//using Consumer.Domain.SeedWork;
using Consumer.Application.Interfaces;
using Consumer.Domain.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using Utils;

namespace Consumer.Application.Behaviours
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;

        public TransactionBehaviour(IUnitOfWork uow, ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is not ITransactionable || _uow.HasActiveTransaction)
                return await next();

            string typeName = request.GetGenericTypeName();

            try
            {
                TResponse response = default;
                IExecutionStrategy strategy = _uow.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    Guid? transactionId = await _uow.BeginTransactionAsync(cancellationToken);
                    using (LogContext.PushProperty("TransactionContext", transactionId))
                    {
                        _logger.LogInformation($"----- Begin transaction {transactionId} for {typeName} ({request})");

                        response = await next();

                        _logger.LogInformation($"----- Commit transaction {transactionId} for {typeName}");

                        await _uow.CommitTransactionAsync(transactionId, cancellationToken);
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