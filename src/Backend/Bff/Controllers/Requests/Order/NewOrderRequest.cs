namespace Bff.Controllers.Requests.Order
{
    /// <summary>
    /// DTO for creating a new order.
    /// </summary>
    public class NewOrderRequest
    {
        /// <summary>
        /// The list of order details.
        /// </summary>
        public List<NewOrderDetailRequest> OrderDetails { get; set; } = [];
    }
}
