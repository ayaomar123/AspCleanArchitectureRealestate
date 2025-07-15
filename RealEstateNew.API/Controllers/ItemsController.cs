using Microsoft.AspNetCore.Mvc;
using RealEstateNew.Application.DTOs;
using RealEstateNew.Application.Interfaces.Item;

namespace RealEstateNew.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _service;

        public ItemsController(IItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return data == null ? NotFound() : Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ShowAsync(int id)
        {
            var data = await _service.ShowAsync(id);
            return data == null ? NotFound() : Ok(data);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ItemRequestDto dto)
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
        public async Task<IActionResult> Update(int id, [FromForm] ItemRequestDto dto)
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
