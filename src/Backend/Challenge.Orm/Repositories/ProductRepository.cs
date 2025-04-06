using Challenge.Domain.Entities;
using Challenge.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Challenge.Orm.Repositories
{
    public class ProductRepository : BasicRepository<Product, ProductRepository>, IProductRepository
    {
        public ProductRepository(AppDbContext context, ILogger<ProductRepository> logger) : base(context, context.Set<Product>(), logger)
        {
        }

        public Task<Product> GetProductAsync(Guid resellerId, int productId, bool AsNonTracking = true)
        {
            var product = _dbSet.Where(x => x.ResellerId == resellerId && x.Id == productId);
            if (AsNonTracking)
                product.AsNoTracking();
            return product.FirstAsync();
        }

        public Product GetProductById(int productId, bool AsNonTracking = true)
        {
            var product = _dbSet.Where(x => x.Id == productId);
            if (AsNonTracking)
                product.AsNoTracking();
            return product.First();
        }

        public Task<List<Product>> GetProductsAsync(Guid resellerId, bool AsNonTracking = true)
        {
            var products = _dbSet.Where(x => x.ResellerId == resellerId);
            if (AsNonTracking)
                products = products.AsNoTracking();
            return products.ToListAsync();
        }

        public async Task<Product> SaveProductAsync(Product request)
        {
            var @return = await _dbSet.AddAsync(request);
            await _context.SaveChangesAsync();
            return @return.Entity;
        }
    }
}
