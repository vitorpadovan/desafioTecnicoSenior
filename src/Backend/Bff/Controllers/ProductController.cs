
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Bff.Controllers
{
    
    public class ProductController : AppBaseController<ProductController>
    {
        public ProductController(ILogger<ProductController> logger, IMapper mapper) : base(logger, mapper)
        {
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Index([FromRoute] Guid id)
        {
            return Ok();
        }
    }
}
