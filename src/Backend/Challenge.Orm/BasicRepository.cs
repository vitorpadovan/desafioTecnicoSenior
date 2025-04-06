using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Challenge.Orm
{
    public abstract class BasicRepository<T, G> where T : class where G : BasicRepository<T, G>
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;
        protected readonly ILogger<G> _logger;

        protected BasicRepository(AppDbContext context, DbSet<T> dbSet, ILogger<G> logger)
        {
            _context = context;
            _dbSet = dbSet;
            _logger = logger;
        }
    }
}
