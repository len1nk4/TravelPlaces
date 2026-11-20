namespace TravelPLaces11.Data.Entities
{
    public class TravelPlace
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int AreaId { get; set; }

        public int? PlaceTypeId { get; set; }

        public decimal Fee { get; set; }

        public string? Description { get; set; }

        public Area Area { get; set; } = null!;

        public PlaceType? PlaceType { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
