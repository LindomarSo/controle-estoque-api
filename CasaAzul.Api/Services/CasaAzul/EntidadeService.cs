using AutoMapper;
using CasaAzul.Api.Services.CasaAzul.Interfaces;
using CasaAzul.Api.ViewModels.Entidade;
using CasaAzul.Domain.Interfaces;
using CasaAzul.Domain.Models.Entidade;
using CasaAzul.Domain.Pagination;
using CasaAzul.Domain.Uow;

namespace CasaAzul.Api.Services.CasaAzul
{
    public class EntidadeService : IEntidadeService
    {
        private readonly IEntidadeRepository _entidadeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EntidadeService(IEntidadeRepository entidadeRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _entidadeRepository = entidadeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<EntidadeViewModel> AddAsync(EntidadeViewModel entidade, int userId)
        {
            try
            {
                var model = _mapper.Map<EntidadeModel>(entidade);

                model.UserId = userId;

                if(model.Doacoes.Any())
                    model.Doacoes.ToList().ForEach(x => x.UserId = userId);

                _entidadeRepository.Add(model);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<EntidadeViewModel>(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EntidadeViewModel> UpdateAsync(EntidadeViewModel entidade, int userId)
        {
            try
            {
                var entity = await _entidadeRepository.GetAsync(x => x.Id == entidade.Id);

                if (entity == null)
                    return null;

                var model = _mapper.Map(entidade, entity);
                model.UserId = userId;

                _entidadeRepository.Update(model);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<EntidadeViewModel>(entidade);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<EntidadeViewModel>> GetAllEntidadesBytypeAsync(PageParams pageParams, string pessoa)
        {
            try
            {
                var entidades = await _entidadeRepository.GetAllEntidadesByTypeAsync(pageParams, pessoa);

                var result = _mapper.Map<PageList<EntidadeViewModel>>(entidades);

                result.TamanhoPagina = entidades.TamanhoPagina;
                result.TotalPaginas = entidades.TotalPaginas;
                result.TotalItens = entidades.TotalItens;
                result.PaginaAtual = entidades.PaginaAtual;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EntidadeViewModel> GetEntidadeByIdAsync(int id)
        {
            try
            {
                var entidade = await _entidadeRepository.GetEntidadeByIdAsync(id);

                if (entidade == null)
                    return null;

                return _mapper.Map<EntidadeViewModel>(entidade);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<string> GetEscolaridadeAsync()
        {
            return _entidadeRepository.GetEscolaridadeAsync();
        }

        public IEnumerable<string> GetTipoEntidadeAsync()
        {
            return _entidadeRepository.GetTipoEntidadeAsync();
        }
    }
}
