using Challenge.Domain.Business;
using Challenge.Domain.Entities;
using Challenge.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Challenge.Application.Business
{
    public class ResellerBusiness : IResellerBusiness
    {
        private readonly ILogger<ResellerBusiness> _logger;
        private readonly IResellerRepository _resellerRepository;

        public ResellerBusiness(ILogger<ResellerBusiness> logger,IResellerRepository resellerRepository)
        {
            _resellerRepository = resellerRepository;
            _logger = logger;
        }

        public Task<Reseller> GetResellerAsync(Guid id)
        {
            return _resellerRepository.GetResellerAsync(id);
        }

        public Task<Reseller> SaveResellerAsync(Reseller reseller)
        {
            reseller.State = Domain.Enums.EntityState.Saved;
            return _resellerRepository.SaveResellerAsync(reseller);
        }
    }
}
