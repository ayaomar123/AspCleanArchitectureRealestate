

using RealEstateNew.Application.DTOs;

namespace RealEstateNew.Application.Interfaces.Item.Validation
{
    public interface IItemValidationService
    {
        Task ValidateItemRequestAsync(ItemRequestDto dto);
    }
}
