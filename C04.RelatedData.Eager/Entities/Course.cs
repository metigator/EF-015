namespace EF015.QueryData.Entities
{
    public class Course : Entity
    {
        public string? CourseName { get; set; }
        public decimal Price { get; set; }
        public int HoursToComplete { get; set; }
        public ICollection<Section> Sections { get; set; } = new List<Section>();
    }
}
