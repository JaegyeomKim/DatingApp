using AutoMapper;
using DatingAppAPI.DTOs;
using DatingAppAPI.Entities;
using DatingAppAPI.Extenstions;

namespace DatingAppAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // CreateMap: Where we want to go from Where do we want to go to.
            // ForMember: Define more specific mapping.
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoDto>();
        }
    }
}
