using EF015.QueryData.Data;

namespace C03.TrackingVsNoTracking
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new AppDbContext())
            {
                var section = context.Sections.FirstOrDefault(x => x.Id == 1);

                Console.WriteLine("before changing tracked object");

                Console.WriteLine(section.SectionName);

                section.SectionName = "this is a new section name";

                context.SaveChanges();

                section = context.Sections.FirstOrDefault(x => x.Id == 1);

                Console.WriteLine("after bein changed");

                Console.WriteLine(section.SectionName);
            }


            //using (var context = new AppDbContext())
            //{
            //    var section = context.Sections.AsNoTracking().FirstOrDefault(x => x.Id == 1);

            //    Console.WriteLine("before changing tracked object");

            //    Console.WriteLine(section.SectionName); // BlaBla

            //    section.SectionName = "01A51C05";

            //    context.SaveChanges();

            //    section = context.Sections.FirstOrDefault(x => x.Id == 1);

            //    Console.WriteLine("after bein changed");

            //    Console.WriteLine(section.SectionName);
            //}

            Console.ReadKey();
        }

    }
}
