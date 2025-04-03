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
        [Required]
        public string? Email { get; set; }

        /// <summary>
        /// User`s Password
        /// </summary>
        [Required]
        public string? Password { get; set; }
    }
}
