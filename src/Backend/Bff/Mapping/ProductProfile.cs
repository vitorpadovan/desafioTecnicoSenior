using AutoMapper;
using Bff.Controllers.Requests.Product;
using Bff.Controllers.Response.Product;
using Challenge.Domain.Entities;
using Challenge.Domain.Entities.Dto;

namespace Bff.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<NewProductRequest, Product>();
            CreateMap<Product, ProductResponse>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
        }
    }
}
