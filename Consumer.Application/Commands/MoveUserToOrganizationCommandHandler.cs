using Consumer.Application.Interfaces;
using Consumer.Domain.Aggregates.UserAggregate;
using Consumer.Domain.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Consumer.Application.Commands
{
    public class MoveUserToOrganizationCommandHandler : IRequestHandler<MoveUserToOrganizationCommand>, ITransactionable
    {
        private readonly IUnitOfWork _context;
        private IRepository<User> _userRepository;
        private readonly ILogger<MoveUserToOrganizationCommandHandler> _logger;

        public MoveUserToOrganizationCommandHandler(IUnitOfWork context, ILogger<MoveUserToOrganizationCommandHandler> logger)
        {
            _context = context;
            _userRepository = _context.UserRepository;
            _logger = logger;
        }

        public async Task Handle(MoveUserToOrganizationCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(request.UserGuid, cancellationToken);

            if (user == null)
            {
                throw new NullReferenceException(nameof(user));
            }

            _logger.LogInformation($"----- Moving user [{request.UserGuid}] to Organization [{request.OrganizationGuid}]");

            user.MoveToOrganization(request.OrganizationGuid);

            await _context.SaveEntitiesAsync(cancellationToken);
        }
    }
}
