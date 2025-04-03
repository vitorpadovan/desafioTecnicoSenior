using Microsoft.AspNetCore.Mvc;

namespace Bff.Controllers
{
    public abstract class AppBaseController<T> : ControllerBase
    {
        protected readonly ILogger<T> _logger;

        protected Uri GetBaseUri(string append)
        {
            var baseUri = $"{Request.Scheme}://{Request.Host}{Request.Path}/{append}";
            _logger.LogDebug($"Base URI: {baseUri}");
            return new Uri(baseUri);
        }

        protected AppBaseController(ILogger<T> logger)
        {
            _logger = logger;
        }
    }
}
