

using AutoMapper;
using RealEstateNew.Application.DTOs;
using RealEstateNew.Application.DTOs.Bookings;
using RealEstateNew.Application.DTOs.Items;
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

            CreateMap<Item, ItemResponseDto>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                .ForMember(dest => dest.Bookings, opt => opt.MapFrom(src => src.Bookings))
                ;
            CreateMap<Booking, BookingDto>();


            //ItemImages
            CreateMap<ImageRequestDto, Image>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());

            CreateMap<Image, ImageResponseDto>();

            //Bookings
            CreateMap<BookingRequestDto, Booking>();

            CreateMap<Booking, BookingResponseDto>();

        }
    }
}
