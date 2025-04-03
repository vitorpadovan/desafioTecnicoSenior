using Challenge.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Challenge.Domain.Business
{
    public interface IUserBusiness
    {
        public Task<bool> AddRole(IdentityUser user, UserProfiles profile);
        public Task<bool> AddRole(String id, UserProfiles profile);
        public Task<bool> CreateUser(string email, string user, string password, List<UserProfiles> profiles);
        public Task<IdentityUser> CreateUser(string email, string user, string password);
        public Task<IdentityUser> Login(string email, string user, string password);
        public Task<string> GetToken(IdentityUser user);
        Task CreateAdminUser(string email, string password);
    }
}
