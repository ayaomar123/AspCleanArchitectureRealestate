

using RealEstateNew.Domain.Enum;

namespace RealEstateNew.Application.DTOs.Items
{
    public class BookingDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public DateTime BookingDate { get; set; }
        public string? Notes { get; set; }
        public BookingStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
