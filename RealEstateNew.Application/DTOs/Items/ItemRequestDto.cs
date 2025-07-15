



using System.ComponentModel.DataAnnotations;

namespace RealEstateNew.Application.DTOs
{
    public class ItemRequestDto : BaseRequestDto
    {
        [Required]
        public int AdvertiseNo { get; set; }
        [Required] 
        public int CategoryId { get; set; }
        [Required] 
        public int CityId { get; set; }
        public int? DistrictId { get; set; }
        [Required] 
        public int PropertyTypeId { get; set; }
        [Required] 
        public double Latitude { get; set; }
        [Required] 
        public double Longitude { get; set; }
        [Required] 
        public double Soum { get; set; }
        [Required] 
        public double Limit { get; set; }
        [Required] 
        public double StreetWidth { get; set; }
        [Required] 
        public double Space { get; set; }
        [Required] 
        public double PricePerMeter { get; set; }
        [Required]
        [MaxLength(2000)]
        public string? Description { get; set; }
        //public string? HashedPassword { get; set; }
    }
}
