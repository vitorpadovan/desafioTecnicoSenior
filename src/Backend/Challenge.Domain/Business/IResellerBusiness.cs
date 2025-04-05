using Challenge.Domain.Entities;

namespace Challenge.Domain.Business
{
    public interface IResellerBusiness
    {
        Task<Reseller> GetResellerAsync(Guid id);
        Task<List<Reseller>> GetResellersAsync(bool AsNonTrack = true);
        public Task<Reseller> SaveResellerAsync(Reseller reseller);
    }
}
