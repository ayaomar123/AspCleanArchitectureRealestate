

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RealEstateNew.Application.DTOs
{
    public class BaseRequestDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string? Name { get; set; }

        public IFormFile? Image { get; set; } = null;
    }
}
