using Challenge.Application.Business;
using Challenge.Domain.Business;
using Challenge.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Challenge.UnitTest.Fixtures
{
    public class ResellerBusinessFixture
    {
        public Mock<IResellerRepository> ResellerRepositoryMock { get; } = new();
        public Mock<IUserBusiness> UserBusinessMock { get; } = new();
        public Mock<ILogger<ResellerBusiness>> LoggerMock { get; } = new();
        public ResellerBusiness ResellerBusiness { get; }

        public ResellerBusinessFixture()
        {
            ResellerBusiness = new ResellerBusiness(
                LoggerMock.Object,
                ResellerRepositoryMock.Object,
                UserBusinessMock.Object
            );
        }
    }
}
