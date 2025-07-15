

using System.ComponentModel.DataAnnotations;
using RealEstateNew.Domain.Enum;

namespace RealEstateNew.Domain.Entities
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; } = null!;

        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }

        public DateTime BookingDate { get; set; }

        public string? Notes { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending; //Pending/Approved/Visited/Cancelled

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
