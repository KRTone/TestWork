using Consumer.Domain.SeedWork;

namespace Consumer.Domain.Aggregates.UserAggregate
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetPageAsync(Guid organizationGuid, Paging page, CancellationToken token = default);
    }
}
