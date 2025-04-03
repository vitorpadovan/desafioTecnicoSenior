using AutoMapper;
using Bff.Controllers.Response.User;
using Microsoft.AspNetCore.Identity;

namespace Bff.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<IdentityUser, UserCreatedResponse>();
        }
    }
}
