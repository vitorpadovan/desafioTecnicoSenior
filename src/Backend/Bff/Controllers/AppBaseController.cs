using AutoMapper;
using Challenge.Domain.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace Bff.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class AppBaseController<T> : ControllerBase
    {
        protected readonly ILogger<T> _logger;
        protected readonly IMapper _mapper;
        protected readonly IUserContext _userContext;

        protected AppBaseController(ILogger<T> logger, IMapper mapper, IUserContext userContext)
        {
            _logger = logger;
            _mapper = mapper;
            _userContext = userContext;
        }
        protected Uri GetBaseUri(string append)
        {
            var baseUri = $"{Request.Scheme}://{Request.Host}{Request.Path}/{append}";
            _logger.LogDebug("Base URI:{baseUri}", baseUri);
            return new Uri(baseUri);
        }
        protected Uri GetBaseUri(int append)
        {
            return GetBaseUri(append.ToString());
        }
    }
}
