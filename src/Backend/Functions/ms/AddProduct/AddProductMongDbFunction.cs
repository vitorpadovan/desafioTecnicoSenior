using Challenge.Domain.Entities;
using Challenge.Orm;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Challenge.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Challenge.Domain.Entities.Dto;

namespace AddProduct
{
    public class AddProductMongDbFunction
    {
        private readonly ILogger _logger;

        public AddProductMongDbFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AddProductMongDbFunction>();
        }

        [Function("AddProductMongDbFunction")]
        public void Run([RabbitMQTrigger("add_product_mongo", ConnectionStringSetting = "RabbitMq")] ProductDto myQueueItem)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
