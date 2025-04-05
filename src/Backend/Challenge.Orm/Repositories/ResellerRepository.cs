using Challenge.Domain.Entities;
using Challenge.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Challenge.Orm.Repositories
{
    public class ResellerRepository : BasicRepository<Reseller, ResellerRepository>, IResellerRepository
    {
        public ResellerRepository(AppDbContext context, ILogger<ResellerRepository> logger) : base(context, context.Set<Reseller>(), logger)
        {
        }

        public Task<Reseller> GetResellerByIdAsync(Guid id, bool AsNonTrack = true)
        {
            var query = _dbSet.Where(x => x.Id == id);
            if (AsNonTrack)
                query = query.AsNoTracking();
            return query.FirstAsync();
        }

        public Task<List<Reseller>> GetResellersAsync(bool AsNonTrack = true)
        {
            var query = _dbSet.AsQueryable();
            if (AsNonTrack)
                query = query.AsNoTracking();
            return query.ToListAsync();
        }

        public async Task<Reseller> SaveResellerAsync(Reseller reseller)
        {
            var @return = await _dbSet.AddAsync(reseller);
            await _context.SaveChangesAsync();
            return @return.Entity;
        }
    }
}
