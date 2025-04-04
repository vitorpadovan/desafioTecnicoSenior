
using Challenge.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Challenge.Domain.Business
{
    public interface IOrderBusiness
    {
        Task<Order> SaveOrderAsync(List<OrderDetail> detais, Guid ResellerId, Guid user);
    }
}
