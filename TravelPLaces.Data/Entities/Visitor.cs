namespace TravelPLaces11.Data.Entities
{
    public class Visitor
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
