using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RealEstateNew.Application.DTOs;
using RealEstateNew.Application.Interfaces.Item;
using RealEstateNew.Domain.Entities;
using RealEstateNew.Infrastructure.Data;

namespace RealEstateNew.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly string _webRootPath;

        public ItemRepository(AppDbContext context, IMapper mapper, string webRootPath)
        {
            _context = context;
            _mapper = mapper;
            _webRootPath = webRootPath;
        }

        public async Task<List<ItemResponseDto>> GetAllAsync()
        {
            var entities = await _context
                .Items
                .Include(d => d.Category)
                .Include(d => d.City)
                .Include(d => d.District)
                .Include(d => d.PropertyType)
                .ToListAsync();
            return _mapper.Map<List<ItemResponseDto>>(entities);
        }

        public async Task<ItemResponseDto> CreateAsync(ItemRequestDto dto)
        {
            var entity = _mapper.Map<Item>(dto);

            if (dto.Image != null)
                entity.Image = await SaveImageAsync(dto.Image);

            _context.Items.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<ItemResponseDto>(entity);
        }

        public async Task<ItemResponseDto?> UpdateAsync(int id, ItemRequestDto dto)
        {
            var entity = await _context.Items.FindAsync(id);
            if (entity == null)
                return null;

            _mapper.Map(dto, entity);

            if (dto.Image != null)
                entity.Image = await SaveImageAsync(dto.Image);

            await _context.SaveChangesAsync();

            return _mapper.Map<ItemResponseDto>(entity);
        }

        public async Task<ItemResponseDto?> ToggleStatusAsync(int id)
        {
            var entity = await _context.Items.FindAsync(id);
            if (entity == null)
                return null;

            entity.Status = !entity.Status;
            await _context.SaveChangesAsync();

            return _mapper.Map<ItemResponseDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Items.FindAsync(id);
            if (entity == null)
                return false;

            _context.Items.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<string> SaveImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is required.");

            var uploadsPath = Path.Combine(_webRootPath, "images", "items");

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(uploadsPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return Path.Combine("images", "items", fileName).Replace("\\", "/");
        }
    }
}
