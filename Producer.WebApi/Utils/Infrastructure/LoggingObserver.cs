using MassTransit;

namespace Producer.WebApi.Utils.Infrastructure
{
    public class LoggingObserver : IPublishObserver
    {
        private readonly ILogger<LoggingObserver> _logger;

        public LoggingObserver(ILogger<LoggingObserver> logger)
        {
            _logger = logger;
        }

        public Task PostPublish<T>(PublishContext<T> context) where T : class
        {
            _logger.LogInformation($"Message sent: {context.Message}");
            return Task.CompletedTask;
        }

        public Task PrePublish<T>(PublishContext<T> context) where T : class
        {
            _logger.LogDebug($"Try send a message: {context.Message}");
            return Task.CompletedTask;
        }

        public Task PublishFault<T>(PublishContext<T> context, Exception exception) where T : class
        {
            _logger.LogError($"Message sending error: {context.Message}\n{exception}");
            return Task.CompletedTask;
        }
    }
}