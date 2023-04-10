using Consumer.Domain.Aggregates.UserAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Consumer.Application.Commands
{
    public class MoveUserToOrganizationCommandHandler : IRequestHandler<MoveUserToOrganizationCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<MoveUserToOrganizationCommandHandler> _logger;

        public MoveUserToOrganizationCommandHandler(IUserRepository userRepository, ILogger<MoveUserToOrganizationCommandHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task Handle(MoveUserToOrganizationCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(request.UserGuid, cancellationToken);

            if (user == null)
            {
                return;
            }

            _logger.LogInformation($"----- Moving user [{request.UserGuid}] to Organization [{request.OrganizationGuid}]");

            user.MoveToOrganization(request.OrganizationGuid);

            await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
