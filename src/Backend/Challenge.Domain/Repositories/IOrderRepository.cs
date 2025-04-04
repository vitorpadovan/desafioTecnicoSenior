using Challenge.Domain.Entities;

namespace Challenge.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> SaveOrderAsync(Order order);
    }
}
