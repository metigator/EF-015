using EF015.QueryData.Data;
using Microsoft.EntityFrameworkCore;

namespace C02.ClientVsServerEvaluation
{
    class Program
    {
        public static void Main(string[] args)
        {
            //using (var context = new AppDbContext())
            //{
            //    var courseId = 1;

            //    var result = context.Sections
            //        .Where(x => x.CourseId == courseId)
            //        .Select(x => new
            //        {
            //            Id = x.Id,
            //            Section = x.SectionName
            //        });

            //    //DECLARE @__courseId_0 int = 1;
            //    //SELECT[s].[Id], [s].[SectionName] AS[Section]
            //    //FROM[Sections] AS[s]
            //    //WHERE[s].[CourseId] = @__courseId_0

            //    Console.WriteLine(result.ToQueryString());

            //    foreach (var item in result)
            //    {
            //        Console.WriteLine($"{item.Id} {item.Section}");
            //    }
            //}

            using (var context = new AppDbContext())
            {
                var courseId = 1;

                var result = context.Sections
                    .Where(x => x.CourseId == courseId)
                    .Select(x => new
                    {
                        Id = x.Id,
                        Section = x.SectionName.Substring(4),
                        TotalDays = CalculateTotalDays(x.DateRange.StartDate, x.DateRange.EndDate)
                    });

                //  SELECT [s].[Id], [s].[SectionName], [s].[StartDate], [s].[EndDate]
                //  FROM [Sections] AS [s]
                //  WHERE [s].[CourseId] = @__courseId_0
                Console.WriteLine(result.ToQueryString());

                foreach (var item in result)
                {
                    Console.WriteLine($"{item.Id} {item.Section} ({item.TotalDays})");
                }
            }

            Console.ReadKey();
        }

        private static int CalculateTotalDays(DateOnly startDate, DateOnly endDate)
        {
            return endDate.DayNumber - startDate.DayNumber; // 0001-01-01
        }
    }
}
