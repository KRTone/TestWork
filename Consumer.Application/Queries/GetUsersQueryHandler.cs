using Consumer.Domain.Aggregates.UserAggregate;
using Consumer.Domain.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Consumer.Application.Queries
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<User>>
    {
        private readonly ILogger<GetUsersQueryHandler> _logger;
        private readonly IUserRepository _userRepository;

        public GetUsersQueryHandler(IUnitOfWork context, ILogger<GetUsersQueryHandler> logger)
        {
            _userRepository = context.UserRepository;
            _logger = logger;
        }

        public async Task<List<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"----- Query users of Organization: [{request.OrganizationGuid}] Page:[{request.Paging}]");
            return await _userRepository.GetPageAsync(request.OrganizationGuid, request.Paging, cancellationToken);
        }
    }
}
