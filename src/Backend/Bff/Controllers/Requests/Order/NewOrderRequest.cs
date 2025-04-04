namespace Bff.Controllers.Requests.Order
{
    public class NewOrderRequest
    {
        public List<NewOrderDetailRequest> OrderDetails { get; set; } = [];
    }
}
