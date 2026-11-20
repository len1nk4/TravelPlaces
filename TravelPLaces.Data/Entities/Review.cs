namespace TravelPLaces11.Data.Entities
{
    // Отзив за посещение: свързва посетител с туристически обект
    public class Review
    {
        public int Id { get; set; }

        public int TravelPlaceId { get; set; }

        public int VisitorId { get; set; }

        // Оценка от 1 до 5
        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime VisitDate { get; set; }

        // Navigation properties
        public TravelPlace TravelPlace { get; set; } = null!;

        public Visitor Visitor { get; set; } = null!;
    }
}
