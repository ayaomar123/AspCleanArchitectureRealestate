
using RealEstateNew.Application.DTOs;

namespace RealEstateNew.Application.Interfaces.District
{
    public interface IDistrictService
    {
        Task<List<DistrictResponseDto>> GetAllAsync();
        Task<DistrictResponseDto> CreateAsync(DistrictRequestDto dto);
        Task<DistrictResponseDto?> UpdateAsync(int id, DistrictRequestDto dto);
        Task<DistrictResponseDto?> ToggleStatusAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
