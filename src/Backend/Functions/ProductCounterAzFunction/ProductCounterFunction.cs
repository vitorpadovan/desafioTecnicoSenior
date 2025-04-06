using Challenge.Common.Interfaces;
using Challenge.Domain.Entities;
using Challenge.Domain.Entities.Dto;
using Challenge.Domain.Enums;
using Challenge.Orm;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProductCounterAzFunction
{
    public class ProductCounterFunction
    {
        private readonly ILogger _logger;
        private readonly ICacheService _cacheService;
        private readonly IMessageService _messageService;
        private readonly AppDbContext _appDbContext;

        public ProductCounterFunction(ILoggerFactory loggerFactory, ICacheService cacheService, IMessageService messageService, AppDbContext appDbContext)
        {
            _logger = loggerFactory.CreateLogger<ProductCounterFunction>();
            _cacheService = cacheService;
            _messageService = messageService;
            _appDbContext = appDbContext;
        }

        [Function("ProductCounterFunction")]
        public async Task Run([RabbitMQTrigger("order-recived", ConnectionStringSetting = "RabbitMq")] OrderDetailDto orderDetailItem)
        {
            var id = $"PRODUCT_COUNTER:PRODUCTID:{orderDetailItem.ProductId}";
            var total = await _cacheService.AddCounter(id, orderDetailItem.Quantity);
            if (total >= 1000)
            {
                _logger.LogInformation("Executando o pedido do product {Id} is {Quantity}", id, total);
                await _cacheService.DeleteCounter(id);
                var orders = await _appDbContext.Set<OrderDetail>()
                    .Include(x => x.Order)
                    .Where(x => x.Product.Id == orderDetailItem.ProductId && x.State == OrderItemState.WaintSendToFactory)
                    .ToListAsync();
                var dtos = orders.Select(x => new OrderDetailDto()
                {
                    OrderId = x.Order.Id,
                    ProductId = orderDetailItem.ProductId,
                    Quantity = x.Quantity
                }
                ).ToList();
                orders.ForEach(x => x.State = OrderItemState.SendToFactory);
                await _appDbContext.SaveChangesAsync();
                await _messageService.PublishAsync<List<OrderDetailDto>>(dtos, "request_to_fabric");
            }
            else
            {
                //TODO colocar o waiting aqui
                var order = await _appDbContext.Set<OrderDetail>()
                    .Where(x => x.Product.Id == orderDetailItem.ProductId && x.Order.Id == orderDetailItem.OrderId)
                    .FirstAsync();
                order.State = OrderItemState.WaintSendToFactory;
                await _appDbContext.SaveChangesAsync();
                _logger.LogInformation("Aguardando para fazer o pedido do product {Id} is {Quantity}", id, total);
            }
            _logger.LogInformation("Total quantity for product {Id} is {Quantity}", id, total);
            _logger.LogInformation($"C# Queue trigger function processed: {orderDetailItem}");
        }
    }
}
