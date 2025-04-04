using Challenge.Domain.Business;
using Challenge.Domain.Entities;
using Challenge.Domain.Extension;
using Challenge.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Challenge.Application.Business
{
    public class ResellerBusiness : IResellerBusiness
    {
        private readonly ILogger<ResellerBusiness> _logger;
        private readonly IResellerRepository _resellerRepository;
        private readonly IUserBusiness _userBusiness;

        public ResellerBusiness(ILogger<ResellerBusiness> logger, IResellerRepository resellerRepository, IUserBusiness userBusiness)
        {
            _logger = logger;
            _resellerRepository = resellerRepository;
            _userBusiness = userBusiness;
        }

        public Task<Reseller> GetResellerAsync(Guid id)
        {
            return _resellerRepository.GetResellerByIdAsync(id);
        }

        public async Task<Reseller> SaveResellerAsync(Reseller reseller)
        {
            reseller.State = Domain.Enums.EntityState.Saved;
            var @return = await _resellerRepository.SaveResellerAsync(reseller);

            //TODO adicionar criação de senha para fornecedores
            await _userBusiness.CreateUserAsync(@return.Email, @return.Email, String.Empty.GeneratePassword());
            this._logger.LogDebug("Salvo no banco a revenda {revenda}", @return);
            return @return;
        }
    }
}
