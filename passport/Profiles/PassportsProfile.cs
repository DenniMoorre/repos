using AutoMapper;
using passport.Dtos;
using passport.Models;

namespace passport.Profiles
{
    public class PassportsProfile : Profile
    {
        public PassportsProfile()
        {
            // Source -> Target
            CreateMap<Passport, PassportReadDto>();
            CreateMap<PassportCreateDto, Passport>();
            
            CreateMap<PassportReadDto, PassportPublishedDto>();
            
            
            CreateMap<Passport, GrpcPassportModel>()
                .ForMember(dest => dest.PassportId, opt => opt.MapFrom(src =>src.Id))
                .ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src =>src.PassportNumber))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>src.Status));
        
        }

    }
}