using AutoMapper;
using Challenge.Application.Business;
using Challenge.Common.Interfaces;
using Challenge.Domain.Repositories;
using Moq;

namespace Challenge.UnitTest.Fixtures
{
    public class ProductBusinessFixture
    {
        public Mock<IProductRepository> ProductRepositoryMock { get; } = new();
        public Mock<IResellerRepository> ResellerRepositoryMock { get; } = new();
        public Mock<IMessageService> MessageServiceMock { get; } = new();
        public Mock<IMapper> MapperMock { get; } = new();
        public ProductBusiness ProductBusiness { get; }

        public ProductBusinessFixture()
        {
            ProductBusiness = new ProductBusiness(
                ProductRepositoryMock.Object,
                ResellerRepositoryMock.Object,
                MessageServiceMock.Object,
                MapperMock.Object
            );
        }
    }
}
