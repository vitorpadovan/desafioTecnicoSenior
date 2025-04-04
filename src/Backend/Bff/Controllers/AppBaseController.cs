using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Bff.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class AppBaseController<T> : ControllerBase
    {
        protected readonly ILogger<T> _logger;
        protected readonly IMapper _mapper;

        protected AppBaseController(ILogger<T> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        protected Uri GetBaseUri(string append)
        {
            var baseUri = $"{Request.Scheme}://{Request.Host}{Request.Path}/{append}";
            _logger.LogDebug($"Base URI: {baseUri}");
            return new Uri(baseUri);
        }
    }
}
