using Challenge.Application.Business;
using Challenge.Domain.Repositories;
using Moq;

namespace Challenge.UnitTest.Fixtures
{
    public class ProductBusinessFixture
    {
        public Mock<IProductRepository> ProductRepositoryMock { get; } = new();
        public Mock<IResellerRepository> ResellerRepositoryMock { get; } = new();
        public ProductBusiness ProductBusiness { get; }

        public ProductBusinessFixture()
        {
            ProductBusiness = new ProductBusiness(
                ProductRepositoryMock.Object,
                ResellerRepositoryMock.Object
            );
        }
    }
}
