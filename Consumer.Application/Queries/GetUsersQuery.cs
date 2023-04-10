using Consumer.Domain.Aggregates.UserAggregate;
using MediatR;

namespace Consumer.Application.Queries
{
    public class GetUsersQuery : IRequest<List<User>>
    {
        public Guid OrganizationGuid { get; init; }
        public Paging Paging { get; init; } = null!;
    }
}