using MediatR;
using Microsoft.Extensions.Logging;
using Producer.Domain.Aggregates.UserAggregate;
using Producer.Domain.Interfaces;

namespace Producer.Application.Commands
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Name { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string? Patronymic { get; init; }
        public string PhoneNumber { get; init; } = null!;
        public string Email { get; init; } = null!;

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
        {
            private readonly ILogger<CreateUserCommand> _logger;
            private readonly IPublisher<User> _publisher;

            public CreateUserCommandHandler(ILogger<CreateUserCommand> logger, IPublisher<User> publisher)
            {
                _logger = logger;
                _publisher = publisher;
            }

            public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                User user = new(Guid.NewGuid(), request.PhoneNumber, request.Email, request.Name, request.LastName, request.Patronymic);

                _logger.LogInformation($"----- Publishing User: {user}");

                await _publisher.PublishAsync(user);

                return user.Guid;
            }
        }
    }
}