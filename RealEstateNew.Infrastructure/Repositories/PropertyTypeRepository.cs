using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RealEstateNew.Application.DTOs;
using RealEstateNew.Application.Interfaces.PropertyType;
using RealEstateNew.Domain.Entities;
using RealEstateNew.Infrastructure.Data;

namespace RealEstateNew.Infrastructure.Repositories
{
    public class PropertyTypeRepository : IPropertyTypeRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly string _webRootPath;

        public PropertyTypeRepository(AppDbContext context, IMapper mapper, string webRootPath)
        {
            _context = context;
            _mapper = mapper;
            _webRootPath = webRootPath;
        }

        public async Task<List<BaseResponseDto>> GetAllAsync()
        {
            var entities = await _context.PropertyTypes.ToListAsync();
            return _mapper.Map<List<BaseResponseDto>>(entities);
        }

        public async Task<BaseResponseDto> CreateAsync(BaseRequestDto dto)
        {
            var entity = _mapper.Map<PropertyType>(dto);

            if (dto.Image != null)
                entity.Image = await SaveImageAsync(dto.Image);

            _context.PropertyTypes.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<BaseResponseDto>(entity);
        }

        public async Task<BaseResponseDto?> UpdateAsync(int id, BaseRequestDto dto)
        {
            var entity = await _context.PropertyTypes.FindAsync(id);
            if (entity == null)
                return null;

            _mapper.Map(dto, entity);

            if (dto.Image != null)
                entity.Image = await SaveImageAsync(dto.Image);

            await _context.SaveChangesAsync();

            return _mapper.Map<BaseResponseDto>(entity);
        }

        public async Task<BaseResponseDto?> ToggleStatusAsync(int id)
        {
            var entity = await _context.PropertyTypes.FindAsync(id);
            if (entity == null)
                return null;

            entity.Status = !entity.Status;
            await _context.SaveChangesAsync();

            return _mapper.Map<BaseResponseDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PropertyTypes.FindAsync(id);
            if (entity == null)
                return false;

            _context.PropertyTypes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<string> SaveImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is required.");

            var uploadsPath = Path.Combine(_webRootPath, "images", "types");

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(uploadsPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return Path.Combine("images", "types", fileName).Replace("\\", "/");
        }
    }
}
