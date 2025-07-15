using Microsoft.AspNetCore.Mvc;
using RealEstateNew.Application.DTOs;
using RealEstateNew.Application.Interfaces.Item.Images;

namespace RealEstateNew.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IItemImageService _service;

        public ImagesController(IItemImageService service)
        {
            _service = service;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ImageRequestDto dto)
        {
            await _service.CreateAsync(dto);
            return Ok();
        }

        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateBase(int id)
        {
            var item = await _service.ToggleStatusAsync(id);

            if (item == null)
                return NotFound();
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ImageRequestDto dto)
        {
            var item = await _service.UpdateAsync(id, dto);

            if (item == null)
                return NotFound();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _service.DeleteAsync(id);
            if (!item)
                return NotFound();
            return NoContent();
        }
    }
}
