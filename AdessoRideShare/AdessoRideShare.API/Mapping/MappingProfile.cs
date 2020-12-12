using AdessoRideShare.Domain;
using AdessoRideShare.Domain.DTO;
using AutoMapper;

namespace AdessoRideShare.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            //CreateMap<ShoppingCart, ShoppingCartDTO>();
            CreateMap<Journey, JourneyDTO>()
         .ForMember(dest =>
             dest.JourneyDate,
             opt => opt.MapFrom(src => src.JourneyDate))
         .ForMember(dest =>
             dest.Description,
             opt => opt.MapFrom(src => src.Description))
         .ReverseMap();

            // Resource to Domain
            //CreateMap<ShoppingCartDTO, ShoppingCart>();
        }
    }
}
