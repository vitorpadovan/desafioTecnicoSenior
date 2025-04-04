using Challenge.Domain.Business;
using Challenge.Domain.Entities;
using Challenge.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Challenge.Application.Business
{
    public class OrderBusiness : IOrderBusiness
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IResellerRepository _resellerRepository;
        private readonly IProductRepository _productRepository;

        public OrderBusiness(IOrderRepository orderRepository, IResellerRepository resellerRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _resellerRepository = resellerRepository;
            _productRepository = productRepository;
        }

        public async Task<Order> SaveOrderAsync(List<OrderDetail> detais, Guid ResellerId, Guid user)
         {
            Order order = new()
            {
                ClientUserId = user,
                Reseller = await _resellerRepository.GetResellerByIdAsync(ResellerId, AsNonTrack: false),
                OrderDetails = [],
                RecivedAt = DateTime.UtcNow,
                Total = 0
            };
            foreach (var item in detais)
            {
                var product = await _productRepository.GetProductAsync(order.Reseller.Id!.Value, item.Product.Id);
                order.Total += product.Price * item.Quantity;
                order.OrderDetails.Add(new OrderDetail
                {
                    Product = product,
                    Quantity = item.Quantity,
                    Price = product.Price
                });
            }
            return await _orderRepository.SaveOrderAsync(order);
        }
    }
}
