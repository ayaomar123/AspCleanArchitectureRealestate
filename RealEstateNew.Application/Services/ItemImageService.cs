

using RealEstateNew.Application.DTOs;
using RealEstateNew.Application.Interfaces.Item.Images;

namespace RealEstateNew.Application.Services
{
    public class ItemImageService : IItemImageService
    {
        private readonly IItemImageRepository _repository;

        public ItemImageService(IItemImageRepository repository)
        {
            _repository = repository;
        }

        public async Task<ImageResponseDto> CreateAsync(ImageRequestDto dto)
        {
            return await _repository.CreateAsync(dto);
        }

        public async Task<ImageResponseDto?> UpdateAsync(int id, ImageRequestDto dto)
        {
            return await _repository.UpdateAsync(id, dto);
        }

        public async Task<ImageResponseDto?> ToggleStatusAsync(int id)
        {
            return await _repository.ToggleStatusAsync(id);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
