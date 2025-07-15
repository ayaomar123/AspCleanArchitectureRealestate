
using RealEstateNew.Application.DTOs;

namespace RealEstateNew.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<List<BaseResponseDto>> GetAllAsync();
        Task<BaseResponseDto> CreateAsync(BaseRequestDto dto);
        Task<BaseResponseDto?> UpdateAsync(int id, BaseRequestDto dto);
        Task<BaseResponseDto?> ToggleStatusAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
