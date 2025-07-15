

using RealEstateNew.Domain.Entities;
using RealEstateNew.Domain.Enum;

namespace RealEstateNew.Application.DTOs.Bookings
{
    public class BookingResponseDto
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; } = null!;
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public DateTime BookingDate { get; set; }
        public string? Notes { get; set; }
        public BookingStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
