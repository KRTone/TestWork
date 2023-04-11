using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using TestWorkEvents.Users;

namespace Producer.Application.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly ILogger<CreateUserCommand> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(ILogger<CreateUserCommand> logger, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            CreateUser user = _mapper.Map<CreateUser>(request);

            _logger.LogInformation($"----- Publishing User: {user}");

            await _publishEndpoint.Publish(user);

            return user.Guid;
        }
    }
}
