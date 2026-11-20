namespace TravelPlaces11.Services.DTOs.Places
{
    public class CreateReviewDto
    {
        public int TravelPlaceId { get; set; }

        public string VisitorName { get; set; } = string.Empty;

        public string VisitorEmail { get; set; } = string.Empty;

        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime VisitDate { get; set; }
    }
}
