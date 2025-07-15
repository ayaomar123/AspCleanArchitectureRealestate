

using System.ComponentModel.DataAnnotations;

namespace RealEstateNew.Application.DTOs
{
    public class DistrictRequestDto : BaseRequestDto
    {
        [Required]
        public int CityId { get; set; }
    }
}
