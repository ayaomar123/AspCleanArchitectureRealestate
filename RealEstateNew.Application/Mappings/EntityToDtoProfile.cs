

using AutoMapper;
using RealEstateNew.Application.DTOs;
using RealEstateNew.Domain.Entities;

namespace RealEstateNew.Application.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<BaseRequestDto, Category>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<Category, BaseResponseDto>();
        }
    }
}
