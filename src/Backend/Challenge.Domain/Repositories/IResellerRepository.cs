
using Challenge.Domain.Entities;

namespace Challenge.Domain.Repositories
{
    public interface IResellerRepository
    {
        Task<Reseller> GetResellerByIdAsync(Guid id, bool AsNonTrack = true);
        Task<List<Reseller>> GetResellersAsync(bool AsNonTrack = true);
        public Task<Reseller> SaveResellerAsync(Reseller reseller);
    }
}
