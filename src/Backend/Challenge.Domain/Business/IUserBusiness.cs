using Challenge.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Challenge.Domain.Business
{
    public interface IUserBusiness
    {
        public Task<bool> AddRoleAsync(IdentityUser user, UserProfiles profile);
        public Task<bool> AddRoleAsync(String id, UserProfiles profile);
        public Task<bool> CreateUserAsync(string email, string user, string password, List<UserProfiles> profiles);
        public Task<IdentityUser> CreateUserAsync(string email, string user, string password);
        public Task<IdentityUser> LoginAsync(string email, string user, string password);
        public Task<string> GetTokenAsync(IdentityUser user);
        public Task CreateAdminUserAsync(string email, string password);
    }
}
