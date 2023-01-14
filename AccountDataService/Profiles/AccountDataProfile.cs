using AutoMapper;
using AccountDataService.Dtos;
using AccountDataService.Models;

namespace AccountDataService.Profiles
{
    public class AccountDataProfile : Profile
    {
        public AccountDataProfile()
        {
            // Source -> Target
            CreateMap<Passport, PassportReadDto>();
            CreateMap<AccountDataCreateDto, AccountData>();
            CreateMap<AccountData, AccountDataReadDto>();
           /* CreateMap<PlatformPublishedDto, Platform>()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.Id));
            CreateMap<GrpcPlatformModel, Platform>()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.PlatformId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Commands, opt => opt.Ignore());
       */ }
    }
}