namespace TravelPlaces11.Services.DTOs.Places
{
    public class CreatePlaceDto
    {
        public string Name { get; set; } = string.Empty;

        public int AreaId { get; set; }

        public int PlaceTypeId { get; set; }

        public decimal Fee { get; set; }

        public string? Description { get; set; }
    }
}
