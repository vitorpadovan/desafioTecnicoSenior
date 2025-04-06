using Challenge.Application.Business;
using Challenge.Domain.Entities;
using Challenge.Domain.Repositories;
using Moq;

namespace Challenge.UnitTest
{
    public class ProductBusinessTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock = new();
        private readonly Mock<IResellerRepository> _resellerRepositoryMock = new();
        private readonly ProductBusiness _productBusiness;

        public ProductBusinessTests()
        {
            _productBusiness = new ProductBusiness(
                _productRepositoryMock.Object,
                _resellerRepositoryMock.Object
            );
        }

        [Fact]
        public async Task SaveProductAsync_ShouldSaveProductSuccessfully()
        {
            // Arrange
            var resellerId = Guid.NewGuid();
            var product = new Product
            {
                Id = 1,
                Name = "Test Product",
                Reseller = new Reseller
                {
                    Id = resellerId,
                    Document = "123456789",
                    RegistredName = "Test Reseller",
                    TradeName = "Test Trade",
                    Email = "reseller@test.com",
                    Addresses = new List<Address>()
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
                .Setup(p => p.SaveProductAsync(product))
                .ReturnsAsync(product);

            // Act
            var result = await _productBusiness.SaveProductAsync(resellerId, product);

            // Assert
            Assert.NotNull(result);
            _productRepositoryMock.Verify(p => p.SaveProductAsync(product), Times.Once);
        }
    }
}
