namespace TravelPLaces11.Data.Entities
{
    // Туристически обект
    public class TravelPlace
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int AreaId { get; set; }

        // Вид на обекта
        public int? PlaceTypeId { get; set; }

        // Входна такса (не може да е отрицателна)
        public decimal Fee { get; set; }

        public string? Description { get; set; }

        // Navigation properties
        public Area Area { get; set; } = null!;

        public PlaceType? PlaceType { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
