
using AutoMapper;
using Bff.Controllers.Requests.User;
using Bff.Controllers.Response.User;
using Challenge.Domain.Business;
using Challenge.Domain.Contexts;
using Challenge.Domain.Enums;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Bff.Controllers
{
    public class UserController : AppBaseController<UserController>
    {
        private readonly IUserBusiness _userBusiness;
        public UserController(ILogger<UserController> logger, IUserBusiness userBusiness, IMapper mapper, IUserContext userContext) : base(logger, mapper, userContext)
        {
            _userBusiness = userBusiness;
        }

        [HttpPost]
        [Route("create-admin")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAdminUser([FromBody] NewUserRegisterRequest login)
        {
            _logger.LogDebug($"CreateAdminUser s: {login.Email} - {login.Password}");
            await _userBusiness.CreateAdminUserAsync(login.Email, login.Password);
            return Created();
        }

        /// <summary>
        /// Used to do login on app
        /// </summary>
        /// <returns>Acess token</returns>
        /// <response code="200">Return acess token to loged user</response>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            _logger.LogDebug($"Login: {login.Email} - {login.Password}");
            var user = await _userBusiness.LoginAsync(login.Email!, login.Email!, login.Password!);
            if (user == null)
                return Unauthorized();
            var token = await _userBusiness.GetTokenAsync(user);
            AccessTokenResponse? response = new()
            {
                AccessToken = token,
                ExpiresIn = 3200,
                RefreshToken = null,
            };
            return Ok(response);
        }

        /// <summary>
        /// Used to create a user
        /// </summary>
        /// <returns>Created user</returns>
        /// <response code="200">Return acess token to loged user</response>
        [HttpPost]
        [Route("singUp")]
        [Authorize(Roles = nameof(UserProfiles.ADMINISTRATOR))]
        public async Task<IActionResult> SingUp([FromBody] NewUserRegisterRequest login)
        {
            _logger.LogDebug($"SingUp: {login.Email} - {login.Password}");
            var response = await _userBusiness.CreateUserAsync(login.Email!, login.Email!, login.Password!);
            var @return = _mapper.Map<UserCreatedResponse>(response);
            return Created(string.Empty, response);
        }
    }
}
