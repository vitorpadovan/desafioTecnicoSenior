using Challenge.Application.Business;
using Challenge.Domain.Entities;
using Challenge.UnitTest.Fixtures;
using Moq;
using Xunit;

namespace Challenge.UnitTest
{
    public class OrderBusinessTests : IClassFixture<OrderBusinessFixture>
    {
        private readonly OrderBusinessFixture _fixture;

        public OrderBusinessTests(OrderBusinessFixture fixture)
        {
            _fixture = fixture;
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
                        Id = 1,
                        Name = "Test Product",
                        Price = 10,
                        Reseller = new Reseller
                        {
                            Id = resellerId,
                            Document = "123456789",
                            RegistredName = "Test Reseller",
                            TradeName = "Test Trade",
                            Email = "reseller@test.com",
                            Addresses = new List<Address>()
                        }
                    },
                    Quantity = 2
                }
            };

            _fixture.ResellerRepositoryMock
                .Setup(r => r.GetResellerByIdAsync(resellerId, false))
                .ReturnsAsync(new Reseller
                {
                    Id = resellerId,
                    Document = "123456789",
                    RegistredName = "Test Reseller",
                    TradeName = "Test Trade",
                    Email = "reseller@test.com",
                    Addresses = new List<Address>()
                });

            _fixture.ProductRepositoryMock
                .Setup(p => p.GetProductAsync(resellerId, 1, true))
                .ReturnsAsync(new Product
                {
                    Id = 1,
                    Name = "Test Product",
                    Price = 10,
                    Reseller = new Reseller
                    {
                        Id = resellerId,
                        Document = "123456789",
                        RegistredName = "Test Reseller",
                        TradeName = "Test Trade",
                        Email = "reseller@test.com",
                        Addresses = new List<Address>()
                    }
                });

            _fixture.OrderRepositoryMock
                .Setup(o => o.SaveOrderAsync(It.IsAny<Order>()))
                .ReturnsAsync(new Order
                {
                    Id = 1,
                    Reseller = new Reseller
                    {
                        Id = resellerId,
                        Document = "123456789",
                        RegistredName = "Test Reseller",
                        TradeName = "Test Trade",
                        Email = "reseller@test.com",
                        Addresses = new List<Address>()
                    },
                    ClientUserId = userId,
                    Total = 20,
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
