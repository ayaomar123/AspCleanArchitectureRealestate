﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RealEstateNew.Application.DTOs;
using RealEstateNew.Application.Interfaces.Category;
using RealEstateNew.Domain.Entities;
using RealEstateNew.Infrastructure.Data;

namespace RealEstateNew.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly string _webRootPath;

        public CategoryRepository(AppDbContext context, IMapper mapper, string webRootPath)
        {
            _context = context;
            _mapper = mapper;
            _webRootPath = webRootPath;
        }

        public async Task<List<BaseResponseDto>> GetAllAsync()
        {
            var entities = await _context.Categories.ToListAsync();
            return _mapper.Map<List<BaseResponseDto>>(entities);
        }

        public async Task<BaseResponseDto> CreateAsync(BaseRequestDto dto)
        {
            var entity = _mapper.Map<Category>(dto);

            if (dto.Image != null)
                entity.Image = await SaveImageAsync(dto.Image);

            _context.Categories.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<BaseResponseDto>(entity);
        }

        public async Task<BaseResponseDto?> UpdateAsync(int id, BaseRequestDto dto)
        {
            var entity = await _context.Categories.FindAsync(id);
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
            var entity = await _context.Categories.FindAsync(id);
            if (entity == null)
                return null;

            entity.Status = !entity.Status;
            await _context.SaveChangesAsync();

            return _mapper.Map<BaseResponseDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Categories.FindAsync(id);
            if (entity == null)
                return false;

            _context.Categories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<string> SaveImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is required.");

            var uploadsPath = Path.Combine(_webRootPath, "images", "categories");

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(uploadsPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return Path.Combine("images", "categories", fileName).Replace("\\", "/");
        }
    }
}
