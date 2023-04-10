using Consumer.Domain.Aggregates.UserAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Consumer.Application.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(IUserRepository userRepository, ILogger<CreateUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            User user = new User(request.Guid, request.PhoneNumber, request.Email, request.Name, request.LastName, request.Patronymic);

            _logger.LogInformation($"----- Creating User: [{user}]");

            _userRepository.Add(user);

            await _userRepository.UnitOfWork.SaveEntitiesAsync();

            return user.Guid;
        }
    }
}
