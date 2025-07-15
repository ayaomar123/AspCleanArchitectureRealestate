namespace RealEstateNew.Domain.Entities
{
    public class Item:BaseEntity
    {
        public int AdvertiseNo { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int CityId { get; set; }
        public City? City { get; set; }
        public int DistrictId { get; set; }
        public District? District { get; set; }
        public int PropertyTypeId { get; set; }
        public PropertyType? PropertyType { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Soum { get; set; }
        public double Limit { get; set; }
        public double StreetWidth { get; set; }
        public double Space { get; set; }
        public double PricePerMeter { get; set; }
        public string? Description { get; set; }
        public string? HashedPassword { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<Image> Images { get; set; } = new();
    }
}
