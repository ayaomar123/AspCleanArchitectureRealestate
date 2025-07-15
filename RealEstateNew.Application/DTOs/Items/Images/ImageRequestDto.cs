



using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RealEstateNew.Application.DTOs
{
    public class ImageRequestDto
    {
        public int ItemId { get; set; }
        [Required]
        public IFormFile? Image { get; set; } = null;
    }
}
