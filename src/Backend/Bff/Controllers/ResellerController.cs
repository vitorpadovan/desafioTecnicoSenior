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

        /// <summary>
        /// Cria um novo revendedor.
        /// </summary>
        /// <param name="request">Dados do revendedor.</param>
        /// <returns>Revendedor criado.</returns>
        /// <response code="201">Revendedor criado com sucesso.</response>
        /// <response code="400">Erro na requisição.</response>
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] NewResellerRequest request)
        {
            var reseller = _mapper.Map<Reseller>(request);
            var response = await _resellerBusiness.SaveResellerAsync(reseller);
            var responseDto = _mapper.Map<ResellerResponse>(response);
            return Created(GetBaseUri(responseDto.Id.ToString()), responseDto);
        }

        /// <summary>
        /// Obtém os detalhes de um revendedor.
        /// </summary>
        /// <param name="id">ID do revendedor.</param>
        /// <returns>Detalhes do revendedor.</returns>
        /// <response code="200">Revendedor encontrado.</response>
        /// <response code="404">Revendedor não encontrado.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> Index([FromRoute] Guid id)
        {
            Reseller @return = await _resellerBusiness.GetResellerAsync(id);
            var responseDto = _mapper.Map<ResellerResponse>(@return);
            return Ok(responseDto);
        }

        /// <summary>
        /// Lista todos os revendedores.
        /// </summary>
        /// <returns>Lista de revendedores.</returns>
        /// <response code="200">Lista retornada com sucesso.</response>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var @return = await _resellerBusiness.GetResellersAsync();
            var responseDto = _mapper.Map<List<ResellerResponse>>(@return);
            return Ok(responseDto);
        }
    }
}
