using AutoMapper;
using Bff.Controllers.Requests.Product;
using Bff.Controllers.Response.Product;
using Challenge.Domain.Business;
using Challenge.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bff.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : AppBaseController<ProductController>
    {
        private readonly IProductBusiness _productBusiness;
        public ProductController(ILogger<ProductController> logger, IMapper mapper, IProductBusiness productBusiness) : base(logger, mapper)
        {
            _productBusiness = productBusiness;
        }

        [HttpPost("{resellerId}")]
        public async Task<IActionResult> Index([FromRoute] Guid resellerId, [FromBody] NewProductRequest request)
        {
            Product p = _mapper.Map<Product>(request);
            ProductResponse response = _mapper.Map<ProductResponse>(await _productBusiness.SaveProductAsync(resellerId, p));
            return Created(this.GetBaseUri(p.Id.ToString()), response);
        }

        [HttpGet("{resellerId}")]
        public async Task<IActionResult> Index([FromRoute] Guid resellerId)
        {
            List<Product> products = await _productBusiness.GetProductsAsync(resellerId);
            List<ProductResponse> response = _mapper.Map<List<ProductResponse>>(products);
            return Ok(response);
        }

        [HttpGet("{resellerId}/{productId}")]
        public async Task<IActionResult> Index([FromRoute] Guid resellerId, [FromRoute] int productId)
        {
            Product product = await _productBusiness.GetProductAsync(resellerId, productId);
            ProductResponse response = _mapper.Map<ProductResponse>(product);
            return Ok(response);
        }
    }
}
