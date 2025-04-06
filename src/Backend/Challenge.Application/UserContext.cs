using Challenge.Domain.Contexts;
using Challenge.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Challenge.Application
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        public UserContext(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<Guid> GetUserIdAsync()
        {
            var usuario = await this.GetUserIdentityAsync();
            return new Guid(usuario.Id);
        }

        public async Task<IdentityUser> GetUserIdentityAsync()
        {
            if (_httpContextAccessor == null)
                throw new BusinessException("Usuário não autenticado");
            if (_httpContextAccessor.HttpContext == null)
                throw new BusinessException("Usuário não autenticado");
            if (!_httpContextAccessor.HttpContext!.User!.Identity!.IsAuthenticated)
                throw new BusinessException("Usuário não autenticado");
            var claim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            var @return = await _userManager.FindByEmailAsync(claim!.Value);
            if (@return == null)
                throw new NotFoundExceptions("Não foram encontrados usuários para este token");
            return @return!;
        }
    }
}
