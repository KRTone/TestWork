using Consumer.Domain.Aggregates.UserAggregate;
using Consumer.Domain.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Consumer.Application.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUnitOfWork _context;
        private readonly IRepository<User> _userRepository;
        private readonly ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(IUnitOfWork context, ILogger<CreateUserCommandHandler> logger)
        {
            _context = context;
            _userRepository = _context.UserRepository;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            User user = new User(request.Guid, request.PhoneNumber, request.Email, request.Name, request.LastName, request.Patronymic);

            _logger.LogInformation($"----- Creating User: [{user}]");

            _userRepository.Add(user);

            await _context.SaveEntitiesAsync();

            return user.Guid;
        }
    }
}
