namespace TravelPlaces11.Services.DTOs.Places
{
    public class PlaceDetailsDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string AreaName { get; set; } = string.Empty;

        public string? PlaceTypeName { get; set; }

        public decimal Fee { get; set; }

        public string? Description { get; set; }

        public double? AverageRating { get; set; }

        public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
    }
}
