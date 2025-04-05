using AutoMapper;
using Bff.Controllers.Requests.Reseller;
using Bff.Controllers.Response.Reseller;
using Challenge.Domain.Business;
using Challenge.Domain.Contexts;
using Challenge.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bff.Controllers
{
    public class ResellerController : AppBaseController<ResellerController>
    {
        private readonly IResellerBusiness _resellerBusiness;
        public ResellerController(ILogger<ResellerController> logger, IResellerBusiness resellerBusiness, IMapper mapper, IUserContext userContext) : base(logger, mapper, userContext)
        {
            _resellerBusiness = resellerBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] NewResellerRequest request)
        {
            var reseller = _mapper.Map<Reseller>(request);
            var response = await _resellerBusiness.SaveResellerAsync(reseller);
            var responseDto = _mapper.Map<ResellerResponse>(response);
            return Created(GetBaseUri(responseDto.Id.ToString()), responseDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Index([FromRoute] Guid id)
        {
            Reseller @return = await _resellerBusiness.GetResellerAsync(id);
            var responseDto = _mapper.Map<ResellerResponse>(@return);
            return Ok(responseDto);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var @return = await _resellerBusiness.GetResellersAsync();
            var responseDto = _mapper.Map<ResellerResponse>(@return);
            return Ok(responseDto);
        }
    }
}
