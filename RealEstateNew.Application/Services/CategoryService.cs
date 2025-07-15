

using RealEstateNew.Application.DTOs;
using RealEstateNew.Application.Interfaces;

namespace RealEstateNew.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BaseResponseDto>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<BaseResponseDto> CreateAsync(BaseRequestDto dto)
        {
            return await _repository.CreateAsync(dto);
        }

        public async Task<BaseResponseDto?> UpdateAsync(int id, BaseRequestDto dto)
        {
            return await _repository.UpdateAsync(id, dto);
        }

        public async Task<BaseResponseDto?> ToggleStatusAsync(int id)
        {
            return await _repository.ToggleStatusAsync(id);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
