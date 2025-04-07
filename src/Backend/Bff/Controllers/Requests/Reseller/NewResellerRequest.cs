using Bff.Validators;
using System.ComponentModel.DataAnnotations;
using MinLengthAttribute = System.ComponentModel.DataAnnotations.MinLengthAttribute;

namespace Bff.Controllers.Requests.Reseller
{
    /// <summary>
    /// DTO for registering a new reseller.
    /// </summary>
    public class NewResellerRequest
    {
        /// <summary>
        /// The CNPJ document of the reseller.
        /// </summary>
        [Cnpj(ErrorMessage = "Invalid CNPJ.")]
        public required string Document { get; set; }

        /// <summary>
        /// The registered name of the reseller.
        /// </summary>
        public required string RegistredName { get; set; }

        /// <summary>
        /// The trade name of the reseller.
        /// </summary>
        public required string TradeName { get; set; }

        /// <summary>
        /// The email of the reseller.
        /// </summary>
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }

        /// <summary>
        /// The list of contacts for the reseller.
        /// </summary>
        public List<ContactRequest> Contacts { get; set; } = [];

        /// <summary>
        /// The list of addresses for the reseller.
        /// </summary>
        [MinLength(1, ErrorMessage = "Addresses must contain at least one item.")]
        public required List<AddressRequest> Addresses { get; set; } = [];
    }
}
