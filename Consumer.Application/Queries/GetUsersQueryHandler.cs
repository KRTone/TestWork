using Consumer.Domain.Aggregates.UserAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Consumer.Application.Queries
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<User>>
    {
        private readonly ILogger<GetUsersQueryHandler> _logger;
        private readonly IUserRepository _userRepository;

        public GetUsersQueryHandler(IUserRepository userRepository, ILogger<GetUsersQueryHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<List<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"----- Query users of Organization: [{request.OrganizationGuid}] Page:[{request.Paging}]");
            return await _userRepository.GetPageAsync(request.OrganizationGuid, request.Paging, cancellationToken);
        }
    }
}
