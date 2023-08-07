using EF015.QueryData.Data;
using Microsoft.EntityFrameworkCore;

namespace C04.RelatedData.Eager
{
    class Program
    {
        public static void Main(string[] args)
        {
            //using (var context = new AppDbContext())
            //{
            //    var sectionId = 1;

            //    var sectionQuery = context.Sections
            //        .Include(x => x.Participants)
            //        .Where(x => x.Id == sectionId);

            //    Console.WriteLine(sectionQuery.ToQueryString());


            //    var section = sectionQuery.FirstOrDefault();
            //    Console.WriteLine($"section: {section.SectionName}");
            //    Console.WriteLine($"--------------------");
            //    foreach (var participant in section.Participants)
            //        Console.WriteLine($"[{participant.Id}] {participant.FName} {participant.LName}");

            //}

            using (var context = new AppDbContext())
            {
                var sectionId = 1;

                var sectionQuery = context.Sections
                    .Include(x => x.Instructor)
                    .ThenInclude(x => x.Office)
                    .Where(x => x.Id == sectionId);

                Console.WriteLine(sectionQuery.ToQueryString());


                var section = sectionQuery.FirstOrDefault();

                Console.WriteLine($"section: {section.SectionName} " +
                    $"[{section.Instructor.FName} " +
                    $"{section.Instructor.LName} " +
                    $"({section.Instructor.Office.OfficeName})]");

            }

            Console.ReadKey();
        }
    }
}
