using RealEstateNew.Application.DTOs;

namespace RealEstateNew.Application.Interfaces.Item.Images
{
    public interface IItemImageRepository
    {
        Task<ImageResponseDto> CreateAsync(ImageRequestDto dto);
        Task<ImageResponseDto?> UpdateAsync(int id, ImageRequestDto dto);
        Task<ImageResponseDto?> ToggleStatusAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
