using EF015.QueryData.Data;

namespace C05.RelatedData.ExplicitLoading
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

                var query = context.Entry(section).Collection(x => x.Participants).Query();

                Console.WriteLine($"section: {section.SectionName}");
                Console.WriteLine($"--------------------");

                foreach (var participant in query)
                    Console.WriteLine($"[{participant.Id}] {participant.FName} {participant.LName}");

            }

            Console.ReadKey();
        }

    }
}
