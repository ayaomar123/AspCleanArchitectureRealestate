

using RealEstateNew.Application.DTOs.Bookings;
using RealEstateNew.Application.Interfaces.Booking;

namespace RealEstateNew.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repository;

        public BookingService(IBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BookingResponseDto>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<BookingResponseDto> CreateAsync(BookingRequestDto dto)
        {
            return await _repository.CreateAsync(dto);
        }

        public async Task<BookingResponseDto?> ToggleStatusAsync(int id, UpdateBookingStatusDto dto)
        {
            return await _repository.ToggleStatusAsync(id,dto);
        }
    }
}
