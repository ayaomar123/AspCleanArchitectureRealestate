using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RealEstateNew.Application.DTOs.Bookings;
using RealEstateNew.Application.Interfaces.Booking;
using RealEstateNew.Domain.Entities;
using RealEstateNew.Infrastructure.Data;

namespace RealEstateNew.Infrastructure.Repositories
{
    public class BookingRepository(AppDbContext context, IMapper mapper) : IBookingRepository
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<BookingResponseDto>> GetAllAsync()
        {
            var entities = await _context.Bookings.ToListAsync();
            return _mapper.Map<List<BookingResponseDto>>(entities);
        }

        public async Task<BookingResponseDto> CreateAsync(BookingRequestDto dto)
        {
            var entity = _mapper.Map<Booking>(dto);
            _context.Bookings.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<BookingResponseDto>(entity);
        }


        public async Task<BookingResponseDto?> ToggleStatusAsync(int id, UpdateBookingStatusDto dto)
        {
            var entity = await _context.Bookings.FindAsync(id);
            if (entity == null)
                return null;

            entity.Status = dto.Status;
            await _context.SaveChangesAsync();

            return _mapper.Map<BookingResponseDto>(entity);
        }


    }
}
