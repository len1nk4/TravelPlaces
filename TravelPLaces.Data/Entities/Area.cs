namespace TravelPLaces11.Data.Entities
{
    // Област
    public class Area
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        // Navigation properties
        public ICollection<TravelPlace> TravelPlaces { get; set; } = new List<TravelPlace>();
    }
}
