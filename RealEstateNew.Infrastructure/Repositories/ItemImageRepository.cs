using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealEstateNew.Application.DTOs;
using RealEstateNew.Application.Interfaces.Item.Images;
using RealEstateNew.Domain.Entities;
using RealEstateNew.Infrastructure.Data;

namespace RealEstateNew.Infrastructure.Repositories
{
    public class ItemImageRepository : IItemImageRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly string _webRootPath;

        public ItemImageRepository(AppDbContext context, IMapper mapper, string webRootPath)
        {
            _context = context;
            _mapper = mapper;
            _webRootPath = webRootPath;
        }

        public async Task<ImageResponseDto> CreateAsync(ImageRequestDto dto)
        {
            var entity = _mapper.Map<Image>(dto);

            if (dto.Image != null)
                entity.ImageUrl = await SaveImageAsync(dto.Image);

            _context.Images.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<ImageResponseDto>(entity);
        }

        public async Task<ImageResponseDto?> UpdateAsync(int id, ImageRequestDto dto)
        {
            var entity = await _context.Images.FindAsync(id);
            if (entity == null)
                return null;

            _mapper.Map(dto, entity);

            if (dto.Image != null)
                entity.ImageUrl = await SaveImageAsync(dto.Image);

            await _context.SaveChangesAsync();

            return _mapper.Map<ImageResponseDto>(entity);
        }

        public async Task<ImageResponseDto?> ToggleStatusAsync(int id)
        {
            var entity = await _context.Images.FindAsync(id);
            if (entity == null)
                return null;

            entity.Status = !entity.Status;
            await _context.SaveChangesAsync();

            return _mapper.Map<ImageResponseDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Images.FindAsync(id);
            if (entity == null)
                return false;

            _context.Images.Remove(entity);
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
