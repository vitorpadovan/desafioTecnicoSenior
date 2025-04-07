using AutoMapper;
using Bff.Controllers.Requests.Product;
using Bff.Controllers.Response.Product;
using Challenge.Domain.Business;
using Challenge.Domain.Contexts;
using Challenge.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bff.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : AppBaseController<ProductController>
    {
        private readonly IProductBusiness _productBusiness;
        public ProductController(ILogger<ProductController> logger, IMapper mapper, IProductBusiness productBusiness, IUserContext userContext) : base(logger, mapper, userContext)
        {
            _productBusiness = productBusiness;
        }

        /// <summary>
        /// Creates a new product for a reseller.
        /// </summary>
        /// <param name="resellerId">Reseller ID.</param>
        /// <param name="request">Product data.</param>
        /// <returns>Product created.</returns>
        /// <response code="201">Product successfully created.</response>
        /// <response code="400">Bad request.</response>
        [HttpPost("{resellerId}")]
        public async Task<IActionResult> Index([FromRoute] Guid resellerId, [FromBody] NewProductRequest request)
        {
            Product p = _mapper.Map<Product>(request);
            ProductResponse response = _mapper.Map<ProductResponse>(await _productBusiness.SaveProductAsync(resellerId, p));
            return Created(this.GetBaseUri(p.Id.ToString()), response);
        }

        /// <summary>
        /// Lists all products of a reseller.
        /// </summary>
        /// <param name="resellerId">Reseller ID.</param>
        /// <returns>List of products.</returns>
        /// <response code="200">List successfully returned.</response>
        /// <response code="404">Reseller not found.</response>
        [HttpGet("{resellerId}")]
        public async Task<IActionResult> Index([FromRoute] Guid resellerId)
        {
            List<Product> products = await _productBusiness.GetProductsAsync(resellerId);
            List<ProductResponse> response = _mapper.Map<List<ProductResponse>>(products);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves the details of a specific product.
        /// </summary>
        /// <param name="resellerId">Reseller ID.</param>
        /// <param name="productId">Product ID.</param>
        /// <returns>Product details.</returns>
        /// <response code="200">Product found.</response>
        /// <response code="404">Product not found.</response>
        [HttpGet("{resellerId}/{productId}")]
        public async Task<IActionResult> Index([FromRoute] Guid resellerId, [FromRoute] int productId)
        {
            Product product = await _productBusiness.GetProductAsync(resellerId, productId);
            ProductResponse response = _mapper.Map<ProductResponse>(product);
            return Ok(response);
        }
    }
}
