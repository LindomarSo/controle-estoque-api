using CasaAzul.Domain.Common;
using CasaAzul.Domain.Interfaces;
using CasaAzul.Domain.Models.Entidade;
using CasaAzul.Domain.Pagination;
using CasaAzul.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CasaAzul.Infra.Repository
{
    public class EntidadeRepository : EntityBaseRepository<EntidadeModel>, IEntidadeRepository
    {
        private readonly EntityContext _context;

        public EntidadeRepository(EntityContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PageList<EntidadeModel>> GetAllEntidadesByTypeAsync(PageParams parametros, string pessoa)
        {
            var person = pessoa.ToLower().Contains("juridica") ? Resources.PESSOA_JURIDICA : Resources.PESSOA_FISICA;

            IQueryable<EntidadeModel> query = _context.Entidades
                                                    .Include(entidade => entidade.User)
                                                    .Include(entidade => entidade.Endereco)
                                                    .Where(entidade => entidade.TipoEntidade == person);

            if (!string.IsNullOrEmpty(parametros.Descricao))
                query = query.Where(x => x.Escolaridade.ToLower().Contains(parametros.Descricao.ToLower())
                                          || x.Documento == parametros.Descricao
                                          || x.Email.ToLower().Contains(parametros.Descricao.ToLower())
                                          || x.Endereco.Cidade.ToLower().Contains(parametros.Descricao.ToLower()));

            return await PageList<EntidadeModel>.CreateAsync(query, parametros.NumeroPagina, parametros.TamanhoPagina);
        }

        public async Task<EntidadeModel> GetEntidadeByIdAsync(int id)
        {
            return  await _context.Entidades.Include(entidade => entidade.Doacoes)
                                            .Include(entidade => entidade.User)
                                            .Include(entidade => entidade.Endereco)
                                            .FirstOrDefaultAsync(entidade => entidade.Id == id);

        }

        public IEnumerable<string> GetEscolaridadeAsync()
        {
            return new List<string>
            {
                Resources.ENSINO_MEDIO_COMPLETO,
                Resources.ENSINO_MEDIO_INCOMPLETO,
                Resources.ENSINO_MEDIO_CURSANDO,
                Resources.ENSINO_SUPERIOR_CURSANDO,
                Resources.ENSINO_SUPERIOR_COMPLETO,
                Resources.ENSINO_SUPERIOR_INCOMPLETO
            };
        }

        public IEnumerable<string> GetTipoEntidadeAsync()
        {
            return new List<string>
            {
                Resources.PESSOA_FISICA,
                Resources.PESSOA_JURIDICA
            };
        }
    }
}
