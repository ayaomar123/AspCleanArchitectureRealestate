

using RealEstateNew.Domain.Entities;

namespace RealEstateNew.Application.DTOs
{
    public class ImageResponseDto 
    {
        public int? Id { get; set; }
        public string? ImageUrl { get; set; }
        public bool? Status { get; set; }
    }
}
