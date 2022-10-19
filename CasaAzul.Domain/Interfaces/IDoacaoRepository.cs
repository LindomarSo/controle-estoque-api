using CasaAzul.Domain.Models.Doacao;
using CasaAzul.Domain.Pagination;

namespace CasaAzul.Domain.Interfaces
{
    public interface IDoacaoRepository : IEntityBaseRepository<DoacaoModel>
    {
        Task<DoacaoModel> GetDoacaoByIdAsync(int id);
        Task<PageList<DoacaoModel>> GetAllDoacoesAsync(PageParams parametros);
        IEnumerable<string> GetUnidades();
    }
}
