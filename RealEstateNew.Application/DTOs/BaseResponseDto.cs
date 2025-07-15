

namespace RealEstateNew.Application.DTOs
{
    public class BaseResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string? BackgroundImage { get; set; }
        public bool Status { get; set; }
    }
}
