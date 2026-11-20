namespace TravelPlaces11.Services.DTOs.Places
{
    public class PlaceListDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string AreaName { get; set; } = string.Empty;

        public string? PlaceTypeName { get; set; }

        public decimal Fee { get; set; }

        public string? Description { get; set; }

        // Средна оценка от отзивите (null, ако няма отзиви)
        public double? AverageRating { get; set; }

        public int ReviewsCount { get; set; }
    }
}
