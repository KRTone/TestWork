using Consumer.Domain.Aggregates.OrganizationAggregate;
using Consumer.Domain.Aggregates.UserAggregate;
using Consumer.Domain.SeedWork;
using Consumer.Infastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Consumer.Infastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ConsumerDbContext _context;

        public UserRepository(ConsumerDbContext contex)
        {
            _context = contex;
        }

        public IUnitOfWork UnitOfWork => _context;

        public User Add(User entity)
        {
            return _context.Users.Add(entity).Entity;
        }

        public Task<User?> GetAsync(Guid guid, CancellationToken token = default)
        {
            return _context
                .Users
                .FirstOrDefaultAsync(o => o.Guid == guid, token);
        }

        public async Task<List<User>> GetPageAsync(Guid organizationGuid, Paging paging, CancellationToken token = default)
        {
            Organization? organization = await _context
                .Organizations
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Guid == organizationGuid, token);

            if (organization == null)
            {
                return new();
            }

            EntityEntry<Organization> entry = _context.Entry(organization);

            return await entry
                .Collection(x => x.Users)
                .Query()
                .AsNoTracking()
                .Skip(paging.Skip)
                .Take(paging.Count)
                .ToListAsync();
        }
    }
}