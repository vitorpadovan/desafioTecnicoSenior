using Microsoft.AspNetCore.Identity;

namespace Bff.Controllers.Response.User
{
    public class UserCreatedResponse
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public static implicit operator UserCreatedResponse(IdentityUser v)
        {
            return new()
            {
                Email = v.Email!,
                Username = v.UserName!,
            };
        }
    }
}
