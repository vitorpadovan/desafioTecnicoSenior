using AutoMapper;
using Bff.Controllers.Requests.Order;
using Bff.Controllers.Response.Order;
using Challenge.Domain.Business;
using Challenge.Domain.Contexts;
using Challenge.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bff.Controllers
{
    [Authorize]
    public class OrderController : AppBaseController<ProductController>
    {
        private readonly IOrderBusiness _orderBusiness;
        public OrderController(ILogger<ProductController> logger, IMapper mapper, IOrderBusiness orderBusiness, IUserContext userContext) : base(logger, mapper, userContext)
        {
            _orderBusiness = orderBusiness;
        }

        [HttpPost("{resellerId}")]
        public async Task<IActionResult> Index([FromBody] NewOrderRequest request, [FromRoute] Guid resellerId)
        {
            List<OrderDetail> orderRequest = _mapper.Map<List<OrderDetail>>(request.OrderDetails);
            var user = await _userContext.GetUserIdAsync();
            Order order = await _orderBusiness.SaveOrderAsync(orderRequest, resellerId, user);
            NewOrderResponse response = _mapper.Map<NewOrderResponse>(order);
            return Created(GetBaseUri(order.Id), response);
        }
    }
}
