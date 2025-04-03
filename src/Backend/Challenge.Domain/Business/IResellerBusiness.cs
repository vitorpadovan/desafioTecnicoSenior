using Challenge.Domain.Entities;

namespace Challenge.Domain.Business
{
    public interface IResellerBusiness
    {
        Task<Reseller> GetResellerAsync(Guid id);
        public Task<Reseller> SaveResellerAsync(Reseller reseller);
    }
}
