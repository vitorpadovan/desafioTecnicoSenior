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
        /// Cria um novo produto para um revendedor.
        /// </summary>
        /// <param name="resellerId">ID do revendedor.</param>
        /// <param name="request">Dados do produto.</param>
        /// <returns>Produto criado.</returns>
        /// <response code="201">Produto criado com sucesso.</response>
        /// <response code="400">Erro na requisição.</response>
        [HttpPost("{resellerId}")]
        public async Task<IActionResult> Index([FromRoute] Guid resellerId, [FromBody] NewProductRequest request)
        {
            Product p = _mapper.Map<Product>(request);
            ProductResponse response = _mapper.Map<ProductResponse>(await _productBusiness.SaveProductAsync(resellerId, p));
            return Created(this.GetBaseUri(p.Id.ToString()), response);
        }

        /// <summary>
        /// Lista todos os produtos de um revendedor.
        /// </summary>
        /// <param name="resellerId">ID do revendedor.</param>
        /// <returns>Lista de produtos.</returns>
        /// <response code="200">Lista retornada com sucesso.</response>
        /// <response code="404">Revendedor não encontrado.</response>
        [HttpGet("{resellerId}")]
        public async Task<IActionResult> Index([FromRoute] Guid resellerId)
        {
            List<Product> products = await _productBusiness.GetProductsAsync(resellerId);
            List<ProductResponse> response = _mapper.Map<List<ProductResponse>>(products);
            return Ok(response);
        }

        /// <summary>
        /// Obtém os detalhes de um produto específico.
        /// </summary>
        /// <param name="resellerId">ID do revendedor.</param>
        /// <param name="productId">ID do produto.</param>
        /// <returns>Detalhes do produto.</returns>
        /// <response code="200">Produto encontrado.</response>
        /// <response code="404">Produto não encontrado.</response>
        [HttpGet("{resellerId}/{productId}")]
        public async Task<IActionResult> Index([FromRoute] Guid resellerId, [FromRoute] int productId)
        {
            Product product = await _productBusiness.GetProductAsync(resellerId, productId);
            ProductResponse response = _mapper.Map<ProductResponse>(product);
            return Ok(response);
        }
    }
}
