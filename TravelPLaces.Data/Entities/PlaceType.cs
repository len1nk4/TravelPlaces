namespace TravelPLaces11.Data.Entities
{
    public class PlaceType
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<TravelPlace> TravelPlaces { get; set; } = new List<TravelPlace>();
    }
}
