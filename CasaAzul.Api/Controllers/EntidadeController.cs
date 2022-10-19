using CasaAzul.Api.Extensions;
using CasaAzul.Api.Services.CasaAzul.Interfaces;
using CasaAzul.Api.ViewModels.Entidade;
using CasaAzul.Domain.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CasaAzul.Api.Controllers
{
    [Route("api/v1/entidade")]
    [ApiController]
    [Authorize]
    public class EntidadeController : ControllerBase
    {
        private readonly IEntidadeService _entidadeService;

        public EntidadeController(IEntidadeService entidadeService)
        {
            _entidadeService = entidadeService;
        }

        /// <summary>
        /// Método responsável por retornar todas as entidades
        /// </summary>
        /// <param name="pageParams"></param>
        /// <returns></returns>
        [HttpGet("fisica")]
        public async Task<IActionResult> GetPessoaFisica([FromQuery]PageParams pageParams)
        {
            var entidades = await _entidadeService.GetAllEntidadesFisicaAsync(pageParams);

            Response.AddPaginationHeader(entidades.PaginaAtual, entidades.TotalItens,
                                        entidades.TotalPaginas, entidades.TamanhoPagina);

            return Ok(entidades);
        }

        /// <summary>
        /// Método responsável por retornar todas as entidades
        /// </summary>
        /// <param name="pageParams"></param>
        /// <returns></returns>
        [HttpGet("juridica")]
        public async Task<IActionResult> GetPessoaJuridica([FromQuery]PageParams pageParams)
        {
            var entidades = await _entidadeService.GetAllEntidadesJuridicaAsync(pageParams);

            Response.AddPaginationHeader(entidades.PaginaAtual, entidades.TotalItens,
                                        entidades.TotalPaginas, entidades.TamanhoPagina);

            return Ok(entidades);
        }

        /// <summary>
        /// Método responsável por retornar uma entidade pelo Id
        /// </summary>
        /// <param name="roleView"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _entidadeService.GetEntidadeByIdAsync(id));
        }

        /// <summary>
        /// Método responsável por retornar tipos de escolaridade
        /// </summary>
        /// <param name="roleView"></param>
        /// <returns></returns>
        [HttpGet("escolaridade")]
        public IActionResult GetEscolaridade()
        {
            return Ok(_entidadeService.GetEscolaridadeAsync());
        }

        /// <summary>
        /// Método responsável por retornar tipos de pessoas 
        /// </summary>
        /// <param name="roleView"></param>
        /// <returns></returns>
        [HttpGet("pessoa")]
        public IActionResult GetPessoa()
        {
            return Ok(_entidadeService.GetTipoEntidadeAsync());
        }

        /// <summary>
        /// Método responsável por criar uma nova entidade
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> Post(EntidadeViewModel entidade)
        {
            return Ok(await _entidadeService.AddAsync(entidade, User.GetUserId()));
        }

        /// <summary>
        /// Método responsável por alterar uma entidade
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        [HttpPut()]
        public async Task<IActionResult> Put(EntidadeViewModel entidade)
        {
            return Ok(await _entidadeService.UpdateAsync(entidade, User.GetUserId()));
        }
    }
}