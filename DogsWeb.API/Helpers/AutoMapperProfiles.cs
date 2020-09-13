using System.Linq;
using AutoMapper;
using DogsWeb.API.Dtos;
using DogsWeb.API.Models;

namespace DogsWeb.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ApplicationUser, UserForList>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => 
                                src.Photos.FirstOrDefault(p => p.isMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<ApplicationUser, UserForDetailed>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => 
                                src.Photos.FirstOrDefault(p => p.isMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
        }
    }
}