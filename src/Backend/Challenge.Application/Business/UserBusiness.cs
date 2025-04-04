using Challenge.Domain.Business;
using Challenge.Domain.Enums;
using Challenge.Domain.Exceptions;
using Challenge.Domain.Extension;
using Challenge.Domain.Service;
using Microsoft.AspNetCore.Identity;

namespace Challenge.Application.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenService _tokenService;

        public UserBusiness(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ITokenService tokenService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }

        //TODO adicionar inicialização de roles no startup
        public async Task CreateAdminUserAsync(string email, string password)
        {
            var result = await _userManager.FindByEmailAsync(email);
            var usersInRole = await _userManager.GetUsersInRoleAsync(UserProfiles.ADMINISTRATOR.ToString());
            if (result != null && usersInRole.Count > 0)
                throw new BusinessException("Usuário já cadastrado");
            if (usersInRole.Count > 0)
            {
                var usuarios = usersInRole.Select(x => x.UserName).ToList();
                throw new BusinessException($"Usuários {String.Join(",", usuarios)} já cadastrados como administrador");
            }
            string role = await CreateRole(UserProfiles.ADMINISTRATOR);
            role = await CreateRole(UserProfiles.COMMONUSER);
            role = await CreateRole(UserProfiles.RESELLER);
            role = await CreateRole(UserProfiles.CLIENT);
            var user = new IdentityUser()
            {
                Email = email,
                UserName = email,
                PasswordHash = password.HashPassword(),
                EmailConfirmed = true,
            };
            var createdUser = await _userManager.CreateAsync(user);
            await AssociateAdminRoles(user);
        }

        private async Task AssociateAdminRoles(IdentityUser user)
        {
            await _userManager.AddToRolesAsync(user, [nameof(UserProfiles.ADMINISTRATOR)]);
            await _userManager.AddToRolesAsync(user, [nameof(UserProfiles.COMMONUSER)]);
            await _userManager.AddToRolesAsync(user, [nameof(UserProfiles.RESELLER)]);
            await _userManager.AddToRolesAsync(user, [nameof(UserProfiles.CLIENT)]);
        }

        private async Task<string> CreateRole(UserProfiles userProfile)
        {
            try
            {
                var role = userProfile.ToString();
                await _roleManager.CreateAsync(new() { Name = role, NormalizedName = role });
                return role;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> CreateUserAsync(string email, string userName, string password, List<UserProfiles> profiles)
        {
            var user = new IdentityUser()
            {
                Email = email,
                UserName = userName,
                PasswordHash = password.HashPassword(),
                EmailConfirmed = true,

            };
            var result = await _userManager.CreateAsync(user);
            var role = UserProfiles.COMMONUSER.ToString();
            await _userManager.AddToRolesAsync(user, profiles.Select(x => x.ToString()));
            return result.Succeeded;
        }

        public async Task<IdentityUser> CreateUserAsync(string email, string userName, string password)
        {
            var user = new IdentityUser()
            {
                Email = email,
                UserName = userName,
                PasswordHash = password.HashPassword(),
                EmailConfirmed = true,
            };
            var resultCreat = await _userManager.CreateAsync(user);
            var role = UserProfiles.CLIENT.ToString();
            var resultAssociate = await _userManager.AddToRolesAsync(user, [role]);
            var resultedUser = await _userManager.FindByEmailAsync(user.Email);
            return resultedUser;
        }

        public async Task<string> GetTokenAsync(IdentityUser user)
        {
            var claims = await _signInManager.ClaimsFactory.CreateAsync(user);
            return _tokenService.GenerateToken(user, claims);
        }

        public async Task<IdentityUser> LoginAsync(string email, string userName, string password)
        {
            var user = new IdentityUser()
            {
                Email = email,
                UserName = userName,
                PasswordHash = password.HashPassword(),
                EmailConfirmed = true
            };
            IdentityUser @return = null;

            var r = await _signInManager.PasswordSignInAsync(userName, password, false, false);
            if (r.Succeeded)
                @return = await _userManager.FindByEmailAsync(email);
            return @return;
        }

        public async Task<bool> AddRoleAsync(IdentityUser user, UserProfiles profile)
        {
            var result = await _userManager.AddToRoleAsync(user, profile.ToString());
            if (result.Succeeded)
                return true;
            else
                throw new Exception(@$"Erro ao tentar adicionar role para o usuário {user.Email} 
com a mensagem: {String.Join(", ", result.Errors.Select(x => x.Description))}");
        }
        public async Task<bool> AddRoleAsync(string id, UserProfiles profile)
        {
            var user = await _userManager.FindByIdAsync(id);
            return await AddRoleAsync(user, profile);
        }
    }
}
