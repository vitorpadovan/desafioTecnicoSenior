
using Challenge.Domain.Entities;

namespace Challenge.Domain.Repositories
{
    public interface IResellerRepository
    {
        Task<Reseller> GetResellerAsync(Guid id);
        Task<Reseller> GetResellerByIdAsync(Guid id);
        public Task<Reseller> SaveResellerAsync(Reseller reseller);
    }
}
