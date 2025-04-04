using AutoMapper;
using Bff.Controllers.Requests.Product;
using Bff.Controllers.Response.Product;
using Challenge.Domain.Entities;

namespace Bff.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<NewProductRequest, Product>();
            CreateMap<Product, ProductResponse>();
        }
    }
}
