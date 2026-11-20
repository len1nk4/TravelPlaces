namespace TravelPlaces11.Services.DTOs.Places
{
    public class ReviewDto
    {
        public string VisitorName { get; set; } = string.Empty;

        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime VisitDate { get; set; }
    }
}
