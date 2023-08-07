using EF015.QueryData.Data;

namespace C06.RelatedData.LazyLoading
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new AppDbContext())
            {
                var sectionId = 1;

                var section = context.Sections
                    .FirstOrDefault(x => x.Id == sectionId);

                foreach (var participant in section.Participants)
                    Console.WriteLine($"[{participant.Id}] {participant.FName} {participant.LName}");

            }

            Console.ReadKey();
        }

    }
}
