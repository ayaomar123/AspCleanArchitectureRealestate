

using System.ComponentModel.DataAnnotations;

namespace RealEstateNew.Application.DTOs.Bookings
{
    public class BookingRequestDto
    {
        public int ItemId { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [MaxLength(10)] 
        public string? Mobile { get; set; }
        [Required]
        public DateTime BookingDate { get; set; }
        public string? Notes { get; set; }

    }
}
