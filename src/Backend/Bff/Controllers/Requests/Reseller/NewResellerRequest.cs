using Bff.Validators;
using MinLengthAttribute = System.ComponentModel.DataAnnotations.MinLengthAttribute;
using System.ComponentModel.DataAnnotations;

namespace Bff.Controllers.Requests.Reseller
{
    public class NewResellerRequest
    {
        [Cnpj(ErrorMessage = "Invalid CNPJ.")]
        public required string Document { get; set; }
        public required string RegistredName { get; set; }
        public required string TradeName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }
        public List<ContactRequest> Contacts { get; set; } = [];
        
        [MinLength(1, ErrorMessage = "Addresses must contain at least one item.")]
        public required List<AddressRequest> Addresses { get; set; } = [];
    }
}
