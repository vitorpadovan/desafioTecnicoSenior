using Challenge.Application;
using Challenge.Application.Business;
using Challenge.Application.Services;
using Challenge.Domain.Business;
using Challenge.Domain.Contexts;
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
            services.AddScoped<IResellerUserBusiness, ResellerUserBusiness>();
            services.AddScoped<IResellerUserRepository, ResellerUserRepository>();
            services.AddScoped<IProductBusiness, ProductBusiness>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderBusiness, OrderBusiness>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserContext, UserContext>();
        }
    }
}
