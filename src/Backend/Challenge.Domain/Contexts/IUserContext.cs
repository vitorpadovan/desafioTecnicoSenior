using Microsoft.AspNetCore.Identity;

namespace Challenge.Domain.Contexts
{
    public interface IUserContext
    {
        public Task<IdentityUser> GetUserIdentityAsync();
        public Task<Guid> GetUserIdAsync();
    }
}
