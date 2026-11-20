namespace TravelPLaces11.Data.Entities
{
    // Вид туристически обект (музей, крепост, манастир...)
    public class PlaceType
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        // Navigation properties
        public ICollection<TravelPlace> TravelPlaces { get; set; } = new List<TravelPlace>();
    }
}
