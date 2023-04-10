using Consumer.Domain.Aggregates.OrganizationAggregate;
using Consumer.Domain.SeedWork;
using Consumer.Infastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Consumer.Infastructure.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ConsumerDbContext _context;

        public OrganizationRepository(ConsumerDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public Organization Add(Organization entity)
        {
            return _context.Organizations.Add(entity).Entity;
        }

        public async Task<Organization?> GetAsync(Guid id, CancellationToken token = default)
        {
            var organization = await _context
                            .Organizations
                            .Include(x => x.Users)
                            .FirstOrDefaultAsync(o => o.Guid == id, token);

            //if (organization != null)
            //{
            //    await _context.Entry(organization)
            //        .Collection(i => i.Users)
            //        .LoadAsync(token);
            //}

            return organization;
        }
    }
}