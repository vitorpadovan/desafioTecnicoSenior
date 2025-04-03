using Microsoft.AspNetCore.Identity;

namespace Bff.Controllers.Response.User
{
    public class UserCreatedResponse
    {
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
