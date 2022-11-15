using CasaAzul.Api.ViewModels.Entidade;
using CasaAzul.Domain.Pagination;

namespace CasaAzul.Api.Services.CasaAzul.Interfaces
{
    public interface IEntidadeService
    {
        Task<EntidadeViewModel> GetEntidadeByIdAsync(int id);
        Task<PageList<EntidadeViewModel>> GetAllEntidadesBytypeAsync(PageParams pageParams, string pessoa);
        Task<EntidadeViewModel> AddAsync(EntidadeViewModel entidade, int userId);
        Task<EntidadeViewModel> UpdateAsync(EntidadeViewModel entidade, int userId);
        IEnumerable<string> GetEscolaridadeAsync();
        IEnumerable<string> GetTipoEntidadeAsync();
    }
}
