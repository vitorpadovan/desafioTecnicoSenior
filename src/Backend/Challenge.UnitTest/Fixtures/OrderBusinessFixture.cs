using Challenge.Application.Business;
using Challenge.Common.Interfaces;
using Challenge.Domain.Repositories;
using Moq;

namespace Challenge.UnitTest.Fixtures
{
    public class OrderBusinessFixture
    {
        public Mock<IOrderRepository> OrderRepositoryMock { get; } = new();
        public Mock<IResellerRepository> ResellerRepositoryMock { get; } = new();
        public Mock<IProductRepository> ProductRepositoryMock { get; } = new();
        public Mock<IMessageService> MessageServiceMock { get; } = new();
        public OrderBusiness OrderBusiness { get; }

        public OrderBusinessFixture()
        {
            OrderBusiness = new OrderBusiness(
                OrderRepositoryMock.Object,
                ResellerRepositoryMock.Object,
                ProductRepositoryMock.Object,
                MessageServiceMock.Object
            );
        }
    }
}
