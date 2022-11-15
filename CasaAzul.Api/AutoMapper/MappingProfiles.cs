using AutoMapper;
using CasaAzul.Api.ViewModels;
using CasaAzul.Api.ViewModels.Doacao;
using CasaAzul.Api.ViewModels.Endereco;
using CasaAzul.Api.ViewModels.Entidade;
using CasaAzul.Domain.Models.Doacao;
using CasaAzul.Domain.Models.Endereco;
using CasaAzul.Domain.Models.Entidade;
using CasaAzul.Domain.Models.Identity;

namespace CasaAzul.Api.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserUpdateViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, LoginViewModel>().ReverseMap();
            CreateMap<Role, RoleViewModel>().ReverseMap();

            CreateMap<EntidadeModel, EntidadeViewModel>().ForMember(x => x.DtNascimento, (src) =>
            {

            }).ReverseMap();

            CreateMap<EntidadeModel, EntidadeInfoViewModel>().ReverseMap();
            CreateMap<EnderecoModel, EnderecoViewModel>().ReverseMap();
            CreateMap<DoacaoModel, DoacaoViewModel>().ReverseMap();
            CreateMap<DoacaoModel, DoacaoInfoViewModel>().ReverseMap();
        }
    }
}
