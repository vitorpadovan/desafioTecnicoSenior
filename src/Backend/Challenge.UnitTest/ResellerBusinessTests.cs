using Bogus;
using Challenge.Domain.Entities;
using Challenge.UnitTest.Fixtures;
using Moq;
using Xunit;
using Bogus;

namespace Challenge.UnitTest
{
    public class ResellerBusinessTests : IClassFixture<ResellerBusinessFixture>
    {
        private readonly ResellerBusinessFixture _fixture;
        private readonly Faker _faker;

        public ResellerBusinessTests(ResellerBusinessFixture fixture)
        {
            _fixture = fixture;
            _faker = new Faker();
        }

        [Fact]
        public async Task SaveResellerAsync_ShouldSaveResellerSuccessfully()
        {
            // Arrange
            var reseller = new Reseller
            {
                Document = _faker.Random.ReplaceNumbers("#########"),
                RegistredName = _faker.Company.CompanyName(),
                TradeName = _faker.Company.CompanySuffix(),
                Email = _faker.Internet.Email(),
                Addresses = new List<Address>()
            };

            _fixture.ResellerRepositoryMock
                .Setup(r => r.SaveResellerAsync(reseller))
                .ReturnsAsync(reseller);

            // Act
            var result = await _fixture.ResellerBusiness.SaveResellerAsync(reseller);

            // Assert
            Assert.NotNull(result);
            _fixture.ResellerRepositoryMock.Verify(r => r.SaveResellerAsync(reseller), Times.Once);
        }
    }
}
