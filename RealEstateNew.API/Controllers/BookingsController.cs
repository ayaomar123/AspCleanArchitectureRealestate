using Microsoft.AspNetCore.Mvc;
using RealEstateNew.Application.DTOs.Bookings;
using RealEstateNew.Application.Interfaces.Booking;

namespace RealEstateNew.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController(IBookingService service) : ControllerBase
    {
        private readonly IBookingService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return data == null ? NotFound() : Ok(data);
        }


        [HttpPost]
        public async Task<IActionResult> Create(BookingRequestDto dto)
        {
            await _service.CreateAsync(dto);
            return Ok();
        }

        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateBase(int id, UpdateBookingStatusDto dto)
        {
            var item = await _service.ToggleStatusAsync(id, dto);

            if (item == null)
                return NotFound();
            return Ok();
        }

    }
}
