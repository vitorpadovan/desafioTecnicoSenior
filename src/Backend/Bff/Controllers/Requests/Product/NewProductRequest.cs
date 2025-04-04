using System.ComponentModel.DataAnnotations;

namespace Bff.Controllers.Requests.Product
{
    public class NewProductRequest
    {
        public required string Name { get; set; }
        public string? Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "The price must be at least 0.01.")]
        public required decimal Price { get; set; }
    }
}
