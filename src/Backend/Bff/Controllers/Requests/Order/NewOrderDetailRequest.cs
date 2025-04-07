namespace Bff.Controllers.Requests.Order
{
    /// <summary>
    /// DTO for the details of an order.
    /// </summary>
    public class NewOrderDetailRequest
    {
        /// <summary>
        /// The ID of the product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// The quantity of the product.
        /// </summary>
        public int Quantity { get; set; }
    }
}
