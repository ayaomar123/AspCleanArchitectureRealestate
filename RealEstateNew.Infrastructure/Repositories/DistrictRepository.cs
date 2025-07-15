using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RealEstateNew.Application.DTOs;
using RealEstateNew.Application.Interfaces.District;
using RealEstateNew.Domain.Entities;
using RealEstateNew.Infrastructure.Data;

namespace RealEstateNew.Infrastructure.Repositories
{
    public class DistrictRepository : IDistrictRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly string _webRootPath;

        public DistrictRepository(AppDbContext context, IMapper mapper, string webRootPath)
        {
            _context = context;
            _mapper = mapper;
            _webRootPath = webRootPath;
        }

        public async Task<List<DistrictResponseDto>> GetAllAsync()
        {
            var entities = await _context.Districts.Include(d => d.City).ToListAsync();
            return _mapper.Map<List<DistrictResponseDto>>(entities);
        }

        public async Task<DistrictResponseDto> CreateAsync(DistrictRequestDto dto)
        {
            var entity = _mapper.Map<District>(dto);

            if (dto.Image != null)
                entity.Image = await SaveImageAsync(dto.Image);

            _context.Districts.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<DistrictResponseDto>(entity);
        }

        public async Task<DistrictResponseDto?> UpdateAsync(int id, DistrictRequestDto dto)
        {
            var entity = await _context.Districts.FindAsync(id);
            if (entity == null)
                return null;

            _mapper.Map(dto, entity);

            if (dto.Image != null)
                entity.Image = await SaveImageAsync(dto.Image);

            await _context.SaveChangesAsync();

            return _mapper.Map<DistrictResponseDto>(entity);
        }

        public async Task<DistrictResponseDto?> ToggleStatusAsync(int id)
        {
            var entity = await _context.Districts.FindAsync(id);
            if (entity == null)
                return null;

            entity.Status = !entity.Status;
            await _context.SaveChangesAsync();

            return _mapper.Map<DistrictResponseDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Districts.FindAsync(id);
            if (entity == null)
                return false;

            _context.Districts.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<string> SaveImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is required.");

            var uploadsPath = Path.Combine(_webRootPath, "images", "districts");

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(uploadsPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return Path.Combine("images", "districts", fileName).Replace("\\", "/");
        }
    }
}
