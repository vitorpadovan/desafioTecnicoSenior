using System;
using Challenge.Domain.Entities;
using Challenge.Orm;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Challenge.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Challenge.Domain.Entities.Dto;

namespace AddProduct_Postgres
{
    public class AddProductPostgresFunction
    {
        private readonly ILogger _logger;
        private readonly AppDbContext appDbContext;

        public AddProductPostgresFunction(ILoggerFactory loggerFactory, AppDbContext appDbContext)
        {
            _logger = loggerFactory.CreateLogger<AddProductPostgresFunction>();
            this.appDbContext = appDbContext;
        }

        [Function("AddProductPostgresFunction")]
        public async Task Run([RabbitMQTrigger("add_product_postgres", ConnectionStringSetting = "RabbitMq")] ProductDto product)
        {
            var dbProduct = await appDbContext.Set<Product>().Where(x => x.Id == product.Id).FirstAsync();
            if (product.Stock < 0)
                dbProduct.State = ProductState.Invalid;
            else
                dbProduct.State = ProductState.Actived;
            await appDbContext.SaveChangesAsync();
            _logger.LogInformation($"C# Queue trigger function processed: {product}");
        }
    }
}
