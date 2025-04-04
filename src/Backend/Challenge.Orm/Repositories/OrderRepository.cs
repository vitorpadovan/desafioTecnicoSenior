using Challenge.Domain.Entities;
using Challenge.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Challenge.Orm.Repositories
{
    public class OrderRepository : BasicRepository<Order, OrderRepository>, IOrderRepository
    {
        public OrderRepository(AppDbContext context, ILogger<OrderRepository> logger) : base(context, context.Set<Order>(), logger)
        {
        }

        public async Task<Order> SaveOrderAsync(Order order)
        {
            var @return = await _dbSet.AddAsync(order);
            await _context.SaveChangesAsync();
            return @return.Entity;
        }
    }
}
