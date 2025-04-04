using Challenge.Domain.Entities;

namespace Challenge.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetProductAsync(Guid resellerId, int productId, bool AsNonTracking = true);
        Task<List<Product>> GetProductsAsync(Guid resellerId, bool AsNonTracking = true);
        Task<Product> SaveProductAsync(Product request);
    }
}
