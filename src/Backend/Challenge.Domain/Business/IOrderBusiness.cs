
using Challenge.Domain.Entities;

namespace Challenge.Domain.Business
{
    public interface IOrderBusiness
    {
        Task<Order> SaveOrderAsync(List<OrderDetail> detais, Guid ResellerId, Guid user);
    }
}
