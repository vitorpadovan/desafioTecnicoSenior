using AutoMapper;
using Bff.Controllers.Requests.Reseller;
using Bff.Controllers.Response.Reseller;
using Challenge.Domain.Entities;

namespace Bff.Mapping
{
    public class NewResellerRequestProfile : Profile
    {
        public NewResellerRequestProfile()
        {
            CreateMap<NewResellerRequest, Reseller>();
            CreateMap<Reseller, NewResellerRequest>();
            CreateMap<Reseller, ResellerResponse>();
            CreateMap<AddressRequest, Address>();
            CreateMap<Address, AddressRequest>();
            CreateMap<ContactRequest, Contact>();
            CreateMap<Contact, ContactRequest>();
        }
    }
}
