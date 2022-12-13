using CasaAzul.Api.Extensions;
using CasaAzul.Api.Services.CasaAzul.Interfaces;
using CasaAzul.Api.ViewModels.Doacao;
using CasaAzul.Domain.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CasaAzul.Api.Controllers
{
    [Route("api/v1/doacao")]
    [ApiController]
    [Authorize]
    public class DoacaoController : ControllerBase
    {
        private readonly IDoacaoService _doacaoService;

        public DoacaoController(IDoacaoService doacaoService)
        {
            _doacaoService = doacaoService;
        }

        /// <summary>
        /// Método responsável por retornar todas as doações
        /// </summary>
        /// <param name="pageParams"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            var doacoes = await _doacaoService.GetAllDoacoesAsync(pageParams);

            Response.AddPaginationHeader(doacoes.PaginaAtual, doacoes.TotalItens, doacoes.TotalPaginas, doacoes.TamanhoPagina);
             
            return Ok(doacoes);
        }

        /// <summary>
        /// Método responsável por retornar uma doação pelo Id
        /// </summary>
        /// <param name="roleView"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _doacaoService.GetDoacaoByIdAsync(id));
        }

        /// <summary>
        /// Método responsável por retornar unidades
        /// </summary>
        /// <param name="roleView"></param>
        /// <returns></returns>
        [HttpGet("unidades")]
        public IActionResult GetUnidades()
        {
            return Ok(_doacaoService.GetUnidades());
        }

        /// <summary>
        /// Método responsável por criar uma nova doação
        /// </summary>
        /// <param name="doacao"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> Post(IEnumerable<DoacaoViewModel> doacao)
        {
            return Ok(await _doacaoService.AddAsync(doacao, User.GetUserId()));
        }

        /// <summary>
        /// Método responsável por criar uma nova doação
        /// </summary>
        /// <param name="doacao"></param>
        /// <returns></returns>
        [HttpPut()]
        public async Task<IActionResult> Put(DoacaoViewModel doacao)
        {
            return Ok(await _doacaoService.UpdateAsync(doacao, User.GetUserId()));
        }
    }
}