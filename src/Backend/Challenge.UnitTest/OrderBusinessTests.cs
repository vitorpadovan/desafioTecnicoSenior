using Bogus;
using Challenge.Domain.Entities;
using Challenge.UnitTest.Fixtures;
using Moq;

namespace Challenge.UnitTest
{
    public class OrderBusinessTests : IClassFixture<OrderBusinessFixture>
    {
        private readonly OrderBusinessFixture _fixture;
        private readonly Faker _faker;

        public OrderBusinessTests(OrderBusinessFixture fixture)
        {
            _fixture = fixture;
            _faker = new Faker();
        }

        [Fact]
        public async Task SaveOrderAsync_ShouldSaveOrderSuccessfully()
        {
            // Arrange
            var resellerId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var orderDetails = new List<OrderDetail>
            {
                new()
                {
                    Product = new Product
                    {
                        Id = _faker.Random.Int(1, 100),
                        Name = _faker.Commerce.ProductName(),
                        Price = _faker.Random.Decimal(1, 100),
                        Reseller = new Reseller
                        {
                            Id = resellerId,
                            Document = _faker.Random.ReplaceNumbers("#########"),
                            RegistredName = _faker.Company.CompanyName(),
                            TradeName = _faker.Company.CompanySuffix(),
                            Email = _faker.Internet.Email(),
                            Addresses = new List<Address>()
                        }
                    },
                    Quantity = _faker.Random.Int(1, 10)
                }
            };

            _fixture.ResellerRepositoryMock
                .Setup(r => r.GetResellerByIdAsync(resellerId, false))
                .ReturnsAsync(new Reseller
                {
                    Id = resellerId,
                    Document = _faker.Random.ReplaceNumbers("#########"),
                    RegistredName = _faker.Company.CompanyName(),
                    TradeName = _faker.Company.CompanySuffix(),
                    Email = _faker.Internet.Email(),
                    Addresses = new List<Address>()
                });

            _fixture.ProductRepositoryMock
                .Setup(p => p.GetProductAsync(resellerId, It.IsAny<int>(), true))
                .ReturnsAsync(new Product
                {
                    Id = _faker.Random.Int(1, 100),
                    Name = _faker.Commerce.ProductName(),
                    Price = _faker.Random.Decimal(1, 100),
                    Reseller = new Reseller
                    {
                        Id = resellerId,
                        Document = _faker.Random.ReplaceNumbers("#########"),
                        RegistredName = _faker.Company.CompanyName(),
                        TradeName = _faker.Company.CompanySuffix(),
                        Email = _faker.Internet.Email(),
                        Addresses = new List<Address>()
                    }
                });

            _fixture.OrderRepositoryMock
                .Setup(o => o.SaveOrderAsync(It.IsAny<Order>()))
                .ReturnsAsync(new Order
                {
                    Id = _faker.Random.Int(1, 100),
                    Reseller = new Reseller
                    {
                        Id = resellerId,
                        Document = _faker.Random.ReplaceNumbers("#########"),
                        RegistredName = _faker.Company.CompanyName(),
                        TradeName = _faker.Company.CompanySuffix(),
                        Email = _faker.Internet.Email(),
                        Addresses = new List<Address>()
                    },
                    ClientUserId = userId,
                    Total = _faker.Random.Decimal(10, 1000),
                    RecivedAt = DateTime.UtcNow
                });

            // Act
            var result = await _fixture.OrderBusiness.SaveOrderAsync(orderDetails, resellerId, userId);

            // Assert
            Assert.NotNull(result);
            _fixture.OrderRepositoryMock.Verify(o => o.SaveOrderAsync(It.IsAny<Order>()), Times.Once);
        }
    }
}
