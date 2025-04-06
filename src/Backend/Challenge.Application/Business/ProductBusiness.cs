using Challenge.Domain.Business;
using Challenge.Domain.Entities;
using Challenge.Domain.Repositories;

namespace Challenge.Application.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IProductRepository _productRepository;
        private readonly IResellerRepository _resellerRepository;

        public ProductBusiness(IProductRepository productRepository, IResellerRepository resellerRepository)
        {
            _productRepository = productRepository;
            _resellerRepository = resellerRepository;
        }

        public Task<Product> GetProductAsync(Guid resellerId, int productId, bool AsNonTracking = true)
        {
            return _productRepository.GetProductAsync(resellerId, productId);
        }

        public Task<List<Product>> GetProductsAsync(Guid resellerId, bool AsNonTracking = true)
        {
            return _productRepository.GetProductsAsync(resellerId, AsNonTracking);
        }

        public async Task<Product> SaveProductAsync(Guid id, Product request)
        {
            var reseller = await _resellerRepository.GetResellerByIdAsync(id, false);
            request.Reseller = reseller;
            return await _productRepository.SaveProductAsync(request);
        }
    }
}
