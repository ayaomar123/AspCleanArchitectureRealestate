
using RealEstateNew.Application.DTOs;

namespace RealEstateNew.Application.Interfaces.City
{
    public interface ICityService
    {
        Task<List<BaseResponseDto>> GetAllAsync();
        Task<BaseResponseDto> CreateAsync(BaseRequestDto dto);
        Task<BaseResponseDto?> UpdateAsync(int id, BaseRequestDto dto);
        Task<BaseResponseDto?> ToggleStatusAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
