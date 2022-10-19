using CasaAzul.Api.ViewModels.Doacao;
using CasaAzul.Domain.Pagination;

namespace CasaAzul.Api.Services.CasaAzul.Interfaces
{
    public interface IDoacaoService
    {
        Task<PageList<DoacaoViewModel>> GetAllDoacoesAsync(PageParams pageParams);
        Task<DoacaoViewModel> GetDoacaoByIdAsync(int id);
        Task<IEnumerable<DoacaoViewModel>> AddAsync(IEnumerable<DoacaoViewModel> doacao, int userId);
        IEnumerable<string> GetUnidades();
    }
}
