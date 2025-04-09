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

        /// <summary>
        /// Creates an administrator user.
        /// </summary>
        /// <param name="login">New user data.</param>
        /// <returns>Administrator user created.</returns>
        /// <response code="201">User successfully created.</response>
        /// <response code="400">Bad request.</response>
        [HttpPost]
        [Route("create-admin")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAdminUser([FromBody] NewUserRegisterRequest login)
        {
            await _userBusiness.CreateAdminUserAsync(login.Email, login.Password);
            return Created();
        }

        /// <summary>
        /// Logs into the system.
        /// </summary>
        /// <param name="login">User credentials.</param>
        /// <returns>Access token.</returns>
        /// <response code="200">Login successful.</response>
        /// <response code="401">Invalid credentials.</response>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
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
        /// Creates a new user.
        /// </summary>
        /// <param name="login">New user data.</param>
        /// <returns>User created.</returns>
        /// <response code="201">User successfully created.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="403">User does not have permission to create users.</response>
        [HttpPost]
        [Route("singUp")]
        [Authorize(Roles = nameof(UserProfiles.ADMINISTRATOR))]
        public async Task<IActionResult> SingUp([FromBody] NewUserRegisterRequest login)
        {
            var response = await _userBusiness.CreateUserAsync(login.Email!, login.Email!, login.Password!);
            var @return = _mapper.Map<UserCreatedResponse>(response);
            return Created(string.Empty, response);
        }
    }
}
