

using AutoMapper;
using RealEstateNew.Application.DTOs;
using RealEstateNew.Domain.Entities;

namespace RealEstateNew.Application.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {

            //Category
            CreateMap<BaseRequestDto, Category>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<Category, BaseResponseDto>();


            //City
            CreateMap<BaseRequestDto, City>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<City, BaseResponseDto>();


            //District
            CreateMap<DistrictRequestDto, District>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<District, DistrictResponseDto>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src!.City!.Name));


            //Item
            CreateMap<ItemRequestDto, Item>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<Item, ItemResponseDto>();


            //ItemImages
            CreateMap<ImageRequestDto, Image>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());

            CreateMap<Image, ImageResponseDto>();

        }
    }
}
