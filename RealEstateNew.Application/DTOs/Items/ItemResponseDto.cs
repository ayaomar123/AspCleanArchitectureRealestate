

using RealEstateNew.Application.DTOs.Items;
using RealEstateNew.Domain.Entities;

namespace RealEstateNew.Application.DTOs
{
    public class ItemResponseDto : BaseResponseDto
    {
        public int AdvertiseNo { get; set; }
        public Category? Category { get; set; }
        public City? City { get; set; }
        public District? District { get; set; }
        public PropertyType? PropertyType { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Soum { get; set; }
        public double Limit { get; set; }
        public double StreetWidth { get; set; }
        public double Space { get; set; }
        public double PricePerMeter { get; set; }
        public string? Description { get; set; }
        public string? HashedPassword { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<ImageResponseDto> Images { get; set; } = new();
        public List<BookingDto> Bookings { get; set; } = new();
    }
}
