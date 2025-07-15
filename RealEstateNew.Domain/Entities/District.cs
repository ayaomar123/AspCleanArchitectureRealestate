namespace RealEstateNew.Domain.Entities
{
    public class District:BaseEntity
    {
        public int CityId { get; set; }
        public City? City { get; set; }
    }
}
