using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Challenge.Domain.Entities.Dto;
using MongoDB.Driver;
using AddProduct_MongoDb;
using Microsoft.Extensions.Configuration;
using Challenge.Orm.Migrations;

namespace AddProduct
{
    public class AddProductMongDbFunction
    {
        private readonly ILogger _logger;
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<ProdutoCol> _produtos;

        public AddProductMongDbFunction(ILoggerFactory loggerFactory, IMongoClient mongoClient)
        {
            //var ccx = configuration.GetConnectionString("MongoDbConnection");
            //MongoClientSettings settings = MongoClientSettings.FromConnectionString(ccx);
            //settings.ConnectTimeout = TimeSpan.FromSeconds(4); // Define o timeout máximo de conexão para 3 segundos
            //var teste = new MongoClient(settings);
            _mongoClient = mongoClient;
            _database = mongoClient.GetDatabase("challenge");
            _produtos = _database.GetCollection<ProdutoCol>("produtos");


            _logger = loggerFactory.CreateLogger<AddProductMongDbFunction>();
        }

        [Function("AddProductMongDbFunction")]
        public void Run([RabbitMQTrigger("add_product_mongo", ConnectionStringSetting = "RabbitMq")] ProductDto productDto)
        {
            if (productDto.Stock > 0)
                _produtos.InsertOne(new ProdutoCol{
                    ProductId = productDto.Id,
                    Name = productDto.Name,
                    Stock = productDto.Stock
                });
            else
                return;
            _logger.LogInformation($"C# Queue trigger function processed: {productDto}");
        }
    }
}
