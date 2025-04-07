using Challenge.Domain.Entities;
using Challenge.UnitTest.Fixtures;
using Moq;
using Xunit;

namespace Challenge.UnitTest
{
    public class ResellerBusinessTests : IClassFixture<ResellerBusinessFixture>
    {
        private readonly ResellerBusinessFixture _fixture;

        public ResellerBusinessTests(ResellerBusinessFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task SaveResellerAsync_ShouldSaveResellerSuccessfully()
        {
            // Arrange
            var reseller = new Reseller
            {
                Document = "123456789",
                RegistredName = "Test Reseller",
                TradeName = "Test Trade",
                Email = "reseller@test.com",
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
