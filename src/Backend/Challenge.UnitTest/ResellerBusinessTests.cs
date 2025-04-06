using Challenge.Application.Business;
using Challenge.Domain.Business;
using Challenge.Domain.Entities;
using Challenge.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Challenge.UnitTest
{
    public class ResellerBusinessTests
    {
        private readonly Mock<IResellerRepository> _resellerRepositoryMock = new();
        private readonly Mock<IUserBusiness> _userBusinessMock = new();
        private readonly Mock<ILogger<ResellerBusiness>> _loggerMock = new();
        private readonly ResellerBusiness _resellerBusiness;

        public ResellerBusinessTests()
        {
            _resellerBusiness = new ResellerBusiness(
                _loggerMock.Object,
                _resellerRepositoryMock.Object,
                _userBusinessMock.Object
            );
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

            _resellerRepositoryMock
                .Setup(r => r.SaveResellerAsync(reseller))
                .ReturnsAsync(reseller);

            // Act
            var result = await _resellerBusiness.SaveResellerAsync(reseller);

            // Assert
            Assert.NotNull(result);
            _resellerRepositoryMock.Verify(r => r.SaveResellerAsync(reseller), Times.Once);
        }
    }
}
