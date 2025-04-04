
using Challenge.Domain.Entities;

namespace Challenge.Domain.Business
{
    public interface IProductBusiness
    {
        public Task<Product> GetProductAsync(Guid resellerId, int productId, bool AsNonTracking = true);
        public Task<List<Product>> GetProductsAsync(Guid resellerId, bool AsNonTracking = true);
        public Task<Product> SaveProductAsync(Guid id, Product request);
    }
}
