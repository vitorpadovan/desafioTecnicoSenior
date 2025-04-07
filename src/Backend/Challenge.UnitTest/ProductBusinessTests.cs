using Challenge.Domain.Entities;
using Challenge.UnitTest.Fixtures;
using Moq;
using Xunit;

namespace Challenge.UnitTest
{
    public class ProductBusinessTests : IClassFixture<ProductBusinessFixture>
    {
        private readonly ProductBusinessFixture _fixture;

        public ProductBusinessTests(ProductBusinessFixture fixture)
        {
            _fixture = fixture;
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
                .Setup(p => p.SaveProductAsync(product))
                .ReturnsAsync(product);

            // Act
            var result = await _fixture.ProductBusiness.SaveProductAsync(resellerId, product);

            // Assert
            Assert.NotNull(result);
            _fixture.ProductRepositoryMock.Verify(p => p.SaveProductAsync(product), Times.Once);
        }
    }
}
