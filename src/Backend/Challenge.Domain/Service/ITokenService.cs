using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Challenge.Domain.Service
{
    public interface ITokenService
    {
        public string GenerateToken(IdentityUser user, ClaimsPrincipal claimsPrincipal);
    }
}
