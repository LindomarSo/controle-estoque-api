using AutoMapper;
using CasaAzul.Api.ViewModels;
using CasaAzul.Domain.Models.Identity;

namespace CasaAzul.Api.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserUpdateViewModel>().ReverseMap();
            CreateMap<User, LoginViewModel>().ReverseMap();
        }
    }
}
