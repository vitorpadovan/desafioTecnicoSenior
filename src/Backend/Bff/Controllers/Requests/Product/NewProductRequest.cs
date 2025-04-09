using System.ComponentModel.DataAnnotations;

namespace Bff.Controllers.Requests.Product
{
    /// <summary>
    /// DTO for registering a new product.
    /// </summary>
    public class NewProductRequest
    {
        /// <summary>
        /// The name of the product.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// The description of the product (optional).
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The price of the product.
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "The price must be at least 0.01.")]
        public required decimal Price { get; set; }

        public int Stock { get; set; } = 0;
    }
}
