using AutoMapper;
using CasaAzul.Api.Services.CasaAzul.Interfaces;
using CasaAzul.Api.ViewModels.Doacao;
using CasaAzul.Domain.Interfaces;
using CasaAzul.Domain.Models.Doacao;
using CasaAzul.Domain.Pagination;
using CasaAzul.Domain.Uow;

namespace CasaAzul.Api.Services.CasaAzul
{
    public class DoacaoService : IDoacaoService
    {
        private readonly IDoacaoRepository _doacaoRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DoacaoService(IDoacaoRepository doacaoRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _doacaoRepository = doacaoRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DoacaoViewModel>> AddAsync(IEnumerable<DoacaoViewModel> doacao, int userId)
        {
            try
            {
                foreach(var item in doacao)
                {
                    if(item.Id == 0)
                    {
                        item.UserId = userId;
                        _doacaoRepository.Add(_mapper.Map<DoacaoModel>(item));
                        _unitOfWork.Commit();
                    }
                    else
                    {
                        var model = await _doacaoRepository.GetAsync(x => x.Id == item.Id);

                        if(model != null)
                        {
                            var result = _mapper.Map(item, model);

                            result.UserId = userId;

                            _doacaoRepository.Update(result);
                            _unitOfWork.Commit();
                        }
                    }
                }

                return _mapper.Map<IEnumerable<DoacaoViewModel>>(
                        await _doacaoRepository.GetManyAsync(x => x.EntidadeId == doacao.Select(x => x.EntidadeId).First()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<DoacaoViewModel>> GetAllDoacoesAsync(PageParams pageParams)
        {
            try
            {
                var doacoes = await _doacaoRepository.GetAllDoacoesAsync(pageParams);

                var result = _mapper.Map<PageList<DoacaoViewModel>>(doacoes);

                result.TamanhoPagina = doacoes.TamanhoPagina;
                result.TotalPaginas = doacoes.TotalPaginas;
                result.TotalItens = doacoes.TotalItens;
                result.PaginaAtual = doacoes.PaginaAtual;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DoacaoViewModel> GetDoacaoByIdAsync(int id)
        {
            try
            {
                var entidade = await _doacaoRepository.GetDoacaoByIdAsync(id);

                if (entidade == null)
                    return null;

                return _mapper.Map<DoacaoViewModel>(entidade);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<string> GetUnidades()
        {
            return _doacaoRepository.GetUnidades();
        }
    }
}
