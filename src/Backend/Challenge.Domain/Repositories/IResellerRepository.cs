
using Challenge.Domain.Entities;

namespace Challenge.Domain.Repositories
{
    public interface IResellerRepository
    {
        Task<Reseller> GetResellerAsync(Guid id);
        public Task<Reseller> SaveResellerAsync(Reseller reseller);
    }
}
