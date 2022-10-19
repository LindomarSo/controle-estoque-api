using CasaAzul.Domain.Models.Entidade;
using CasaAzul.Domain.Pagination;

namespace CasaAzul.Domain.Interfaces
{
    public interface IEntidadeRepository : IEntityBaseRepository<EntidadeModel>
    {
        Task<EntidadeModel> GetEntidadeByIdAsync(int id);
        Task<PageList<EntidadeModel>> GetAllEntidadesFisicaAsync(PageParams parametros);
        Task<PageList<EntidadeModel>> GetAllEntidadesJuridicaAsync(PageParams pageParams);
        IEnumerable<string> GetEscolaridadeAsync();
        IEnumerable<string> GetTipoEntidadeAsync();
    }
}
