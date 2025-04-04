using AutoMapper;
using Bff.Controllers.Requests.Order;
using Bff.Controllers.Response.Order;
using Challenge.Domain.Entities;
using Challenge.Domain.Repositories;

namespace Bff.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, NewOrderResponse>();
            CreateMap<NewOrderRequest, Order>();
            CreateMap<NewOrderDetailRequest, OrderDetail>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => new Product { Id = src.ProductId, Name = String.Empty, Reseller = null }));
        }
    }
}
