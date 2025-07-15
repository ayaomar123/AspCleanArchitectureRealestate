

using RealEstateNew.Application.DTOs;

namespace RealEstateNew.Application.Interfaces.Item
{
    public interface IItemRepository
    {
        Task<List<ItemResponseDto>> GetAllAsync();
        Task<ItemResponseDto?> ShowAsync(int id);
        Task<ItemResponseDto> CreateAsync(ItemRequestDto dto);
        Task<ItemResponseDto?> UpdateAsync(int id, ItemRequestDto dto);
        Task<ItemResponseDto?> ToggleStatusAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
