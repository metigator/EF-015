namespace EF015.QueryData.Entities
{
    public class Section : Entity
    {
        public string? SectionName { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        public int InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
        public int ScheduleId { get; set; }
        public Schedule? Schedule { get; set; }
        public DateRange DateRange { get; set; } = new();
        public TimeSlot TimeSlot { get; set; } = new();
        public List<Participant> Participants { get; set; } = new List<Participant>();
    }

    public class TimeSlot
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public override string ToString()
        {
            return $"{StartTime.ToString("hh\\:mm")} - {EndTime.ToString("hh\\:mm")}";
        }
    }

    public class DateRange
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public override string ToString()
        {
            return $"{StartDate.ToString("yyyy-MM-dd")} - {EndDate.ToString("yyyy-MM-dd")}";
        }
    }
}
