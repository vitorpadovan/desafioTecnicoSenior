using Bogus;
using Challenge.Domain.Entities;
using Challenge.UnitTest.Fixtures;
using Moq;

namespace Challenge.UnitTest
{
    public class ProductBusinessTests : IClassFixture<ProductBusinessFixture>
    {
        private readonly ProductBusinessFixture _fixture;
        private readonly Faker _faker;

        public ProductBusinessTests(ProductBusinessFixture fixture)
        {
            _fixture = fixture;
            _faker = new Faker();
        }

        [Fact]
        public async Task SaveProductAsync_ShouldSaveProductSuccessfully()
        {
            // Arrange
            var resellerId = Guid.NewGuid();
            var product = new Product
            {
                Id = _faker.Random.Int(1, 100),
                Name = _faker.Commerce.ProductName(),
                Reseller = new Reseller
                {
                    Id = resellerId,
                    Document = _faker.Random.ReplaceNumbers("#########"),
                    RegistredName = _faker.Company.CompanyName(),
                    TradeName = _faker.Company.CompanySuffix(),
                    Email = _faker.Internet.Email(),
                    Addresses = new List<Address>()
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
