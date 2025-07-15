

using RealEstateNew.Application.DTOs;
using RealEstateNew.Application.Interfaces.District;

namespace RealEstateNew.Application.Services
{
    public class DistrictService : IDistrictService
    {
        private readonly IDistrictRepository _repository;

        public DistrictService(IDistrictRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<DistrictResponseDto>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<DistrictResponseDto> CreateAsync(DistrictRequestDto dto)
        {
            return await _repository.CreateAsync(dto);
        }

        public async Task<DistrictResponseDto?> UpdateAsync(int id, DistrictRequestDto dto)
        {
            return await _repository.UpdateAsync(id, dto);
        }

        public async Task<DistrictResponseDto?> ToggleStatusAsync(int id)
        {
            return await _repository.ToggleStatusAsync(id);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
