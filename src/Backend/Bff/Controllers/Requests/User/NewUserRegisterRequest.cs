using System.ComponentModel.DataAnnotations;

namespace Bff.Controllers.Requests.User
{
    /// <summary>
    /// Dto for new user on systema
    /// </summary>
    public class NewUserRegisterRequest
    {
        /// <summary>
        /// User`s email
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// User`s Password
        /// </summary>
        public required string Password { get; set; }
    }
}
