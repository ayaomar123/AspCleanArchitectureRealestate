using RealEstateNew.Application.DTOs.Bookings;

namespace RealEstateNew.Application.Interfaces.Booking
{
    public interface IBookingRepository
    {
        Task<List<BookingResponseDto>> GetAllAsync();
        Task<BookingResponseDto> CreateAsync(BookingRequestDto dto);
        Task<BookingResponseDto?> ToggleStatusAsync(int id, UpdateBookingStatusDto dto);
    }
}
