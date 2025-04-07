namespace Bff.Controllers.Requests.User
{
    /// <summary>
    /// DTO for registering a new user in the system.
    /// </summary>
    public class NewUserRegisterRequest
    {
        /// <summary>
        /// The email of the user.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// The password of the user.
        /// </summary>
        public required string Password { get; set; }
    }
}
