

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using RealEstateNew.Application.DTOs;
using RealEstateNew.Application.Interfaces.Item.Validation;
using RealEstateNew.Infrastructure.Data;

namespace RealEstateNew.Infrastructure.Repositories
{
    public class ItemValidationService : IItemValidationService
    {
        private readonly AppDbContext _context;

        public ItemValidationService(AppDbContext context)
        {
            _context = context;
        }
        public async Task ValidateItemRequestAsync(ItemRequestDto dto)
        {
            if (!await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId))
                throw new ValidationException($"CategoryId {dto.CategoryId} does not exist.");

            if (!await _context.Cities.AnyAsync(c => c.Id == dto.CityId))
                throw new ValidationException($"CityId {dto.CityId} does not exist.");

            if (!await _context.Districts.AnyAsync(c => c.Id == dto.DistrictId))
                throw new ValidationException($"DistrictId {dto.DistrictId} does not exist.");

            if (!await _context.PropertyTypes.AnyAsync(c => c.Id == dto.PropertyTypeId))
                throw new ValidationException($"PropertyTypeId {dto.PropertyTypeId} does not exist.");
        }
    }
}
