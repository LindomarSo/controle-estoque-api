using CasaAzul.Api.ViewModels.Entidade;
using CasaAzul.Domain.Pagination;

namespace CasaAzul.Api.Services.CasaAzul.Interfaces
{
    public interface IEntidadeService
    {
        Task<EntidadeViewModel> GetEntidadeByIdAsync(int id);
        Task<PageList<EntidadeViewModel>> GetAllEntidadesFisicaAsync(PageParams pageParams);
        Task<PageList<EntidadeViewModel>> GetAllEntidadesJuridicaAsync(PageParams pageParams);
        Task<EntidadeViewModel> AddAsync(EntidadeViewModel entidade, int userId);
        Task<EntidadeViewModel> UpdateAsync(EntidadeViewModel entidade, int userId);
        IEnumerable<string> GetEscolaridadeAsync();
        IEnumerable<string> GetTipoEntidadeAsync();
    }
}
