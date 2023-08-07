namespace EF015.QueryData.Entities
{
    public class Participant : Entity
    {
        public string? FName { get; set; }
        public string? LName { get; set; }
        public virtual ICollection<Section> Sections { get; set; } = new List<Section>();
    }
}
