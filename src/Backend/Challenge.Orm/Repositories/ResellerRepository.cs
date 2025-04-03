using Challenge.Domain.Entities;
using Challenge.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Orm.Repositories
{
    public class ResellerRepository : BasicRepository<Reseller, ResellerRepository>, IResellerRepository
    {
        public ResellerRepository(AppDbContext context, ILogger<ResellerRepository> logger) : base(context, context.Set<Reseller>(), logger)
        {
        }

        public Task<Reseller> GetResellerAsync(Guid id)
        {
            return _dbSet
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstAsync();
        }

        public async Task<Reseller> SaveResellerAsync(Reseller reseller)
        {
            var @return = await _dbSet.AddAsync(reseller);
            await _context.SaveChangesAsync();
            return @return.Entity;
        }
    }
}
