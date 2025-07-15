using System.ComponentModel.DataAnnotations;

namespace RealEstateNew.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(255)]
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? BackgroundImage { get; set; }
        public bool? Status { get; set; } = true;
    }
}
