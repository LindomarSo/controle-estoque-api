using CasaAzul.Domain.Common;
using CasaAzul.Domain.Interfaces;
using CasaAzul.Domain.Models.Doacao;
using CasaAzul.Domain.Pagination;
using CasaAzul.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CasaAzul.Infra.Repository
{
    public class DoacaoRepository : EntityBaseRepository<DoacaoModel>, IDoacaoRepository
    {
        private readonly EntityContext _context;

        public DoacaoRepository(EntityContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PageList<DoacaoModel>> GetAllDoacoesAsync(PageParams parametros)
        {
            IQueryable<DoacaoModel> query = _context.Doacoes
                                                    .Include(doacao => doacao.Entidade)
                                                    .Include(doacao => doacao.User);
                                                              

            return await PageList<DoacaoModel>.CreateAsync(query, parametros.NumeroPagina, parametros.TamanhoPagina);
        }

        public async Task<DoacaoModel> GetDoacaoByIdAsync(int id)
        {
            return await _context.Doacoes.Include(doacao => doacao.Entidade)
                                            .Include(doacao => doacao.User)
                                            .FirstOrDefaultAsync(entidade => entidade.Id == id);
        }

        public IEnumerable<string> GetUnidades()
        {
            return new List<string>
            {
                Resources.RIACHO_FUNDO_II,
                Resources.SAMAMBAIA_SUL
            };
        }
    }
}
