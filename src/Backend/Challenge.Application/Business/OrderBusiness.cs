using Challenge.Common.Interfaces;
using Challenge.Domain.Business;
using Challenge.Domain.Entities;
using Challenge.Domain.Entities.Dto;
using Challenge.Domain.Repositories;

namespace Challenge.Application.Business
{
    public class OrderBusiness : IOrderBusiness
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IResellerRepository _resellerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMessageService _messageService;

        public OrderBusiness(IOrderRepository orderRepository, IResellerRepository resellerRepository, IProductRepository productRepository, IMessageService messageService)
        {
            _orderRepository = orderRepository;
            _resellerRepository = resellerRepository;
            _productRepository = productRepository;
            _messageService = messageService;
        }

        public async Task<Order> SaveOrderAsync(List<OrderDetail> detais, Guid ResellerId, Guid user)
        {
            List<Task> tasks = [];
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
                var orderDetail = new OrderDetail
                {
                    Product = product,
                    Quantity = item.Quantity,
                    Price = product.Price
                };
                order.OrderDetails.Add(orderDetail);
            }
            var @return = await _orderRepository.SaveOrderAsync(order);
            foreach (var item in order.OrderDetails)
            {
                var orderDetailDto = new OrderDetailDto
                {
                    OrderId = @return.Id,
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity
                };
                tasks.Add(_messageService.PublishAsync<OrderDetailDto>(orderDetailDto, queue: "order-recived", routingKey: "order-recived"));
            }
            await Task.WhenAll(tasks);
            return @return;
        }
    }
}
