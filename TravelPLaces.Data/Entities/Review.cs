namespace TravelPLaces11.Data.Entities
{
    public class Review
    {
        public int Id { get; set; }

        public int TravelPlaceId { get; set; }

        public int VisitorId { get; set; }

        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime VisitDate { get; set; }

        public TravelPlace TravelPlace { get; set; } = null!;

        public Visitor Visitor { get; set; } = null!;
    }
}
