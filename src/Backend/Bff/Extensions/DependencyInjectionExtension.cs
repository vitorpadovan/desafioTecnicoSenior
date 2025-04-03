using Challenge.Application.Business;
using Challenge.Application.Services;
using Challenge.Domain.Business;
using Challenge.Domain.Repositories;
using Challenge.Domain.Service;
using Challenge.Orm.Repositories;

namespace Bff.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void AddDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<IResellerBusiness, ResellerBusiness>();
            services.AddScoped<IResellerRepository, ResellerRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserBusiness, UserBusiness>();
        }
    }
}
