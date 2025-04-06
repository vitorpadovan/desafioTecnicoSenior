using Challenge.Application.Business;
using Challenge.Common.Interfaces;
using Challenge.Domain.Entities;
using Challenge.Domain.Repositories;
using Moq;

namespace Challenge.UnitTest
{
    public class OrderBusinessTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock = new();
        private readonly Mock<IResellerRepository> _resellerRepositoryMock = new();
        private readonly Mock<IProductRepository> _productRepositoryMock = new();
        private readonly Mock<IMessageService> _messageServiceMock = new();
        private readonly OrderBusiness _orderBusiness;

        public OrderBusinessTests()
        {
            _orderBusiness = new OrderBusiness(
                _orderRepositoryMock.Object,
                _resellerRepositoryMock.Object,
                _productRepositoryMock.Object,
                _messageServiceMock.Object
            );
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

            _resellerRepositoryMock
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

            _productRepositoryMock
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

            _orderRepositoryMock
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
            var result = await _orderBusiness.SaveOrderAsync(orderDetails, resellerId, userId);

            // Assert
            Assert.NotNull(result);
            _orderRepositoryMock.Verify(o => o.SaveOrderAsync(It.IsAny<Order>()), Times.Once);
        }
    }
}
