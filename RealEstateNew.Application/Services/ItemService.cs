

using RealEstateNew.Application.DTOs;
using RealEstateNew.Application.Interfaces.Item;
using RealEstateNew.Application.Interfaces.Item.Validation;

namespace RealEstateNew.Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _repository;
        private readonly IItemValidationService _validationService;

        public ItemService(IItemRepository repository, IItemValidationService validationService)
        {
            _repository = repository;
            _validationService = validationService;
        }

        public async Task<List<ItemResponseDto>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ItemResponseDto?> ShowAsync(int id)
        {
            return await _repository.ShowAsync(id);
        }

        public async Task<ItemResponseDto> CreateAsync(ItemRequestDto dto)
        {
            await _validationService.ValidateItemRequestAsync(dto);
            return await _repository.CreateAsync(dto);
        }

        public async Task<ItemResponseDto?> UpdateAsync(int id, ItemRequestDto dto)
        {
            await _validationService.ValidateItemRequestAsync(dto);
            return await _repository.UpdateAsync(id, dto);
        }

        public async Task<ItemResponseDto?> ToggleStatusAsync(int id)
        {
            return await _repository.ToggleStatusAsync(id);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
