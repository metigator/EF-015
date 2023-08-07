using EF015.QueryData.Data;
using Microsoft.EntityFrameworkCore;

namespace C01.QueryData
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new AppDbContext())
            {
                //var courses = context.Courses;

                //Console.WriteLine(courses.ToQueryString());

                //foreach (var course in courses)
                //    Console.WriteLine($"course name: {course.CourseName}, {course.HoursToComplete} hrs., {course.Price.ToString("C")}");

                //var course = context.Courses.Single(x => x.Id == 1);

                //Console.WriteLine($"course name: {course.CourseName}, {course.HoursToComplete} hrs., {course.Price.ToString("C")}");

                //var course = context.Courses.Single(x => x.HoursToComplete == 25);

                //Console.WriteLine($"{course.CourseName}, {course.Price.ToString("C")}");

                // var course = context.Courses.First(x => x.HoursToComplete == 25);

                // Console.WriteLine($"{course.CourseName}, {course.Price.ToString("C")}");

                // var course = context.Courses.Single(x => x.HoursToComplete == 999);

                // Console.WriteLine($"{course.CourseName}, {course.Price.ToString("C")}");

                //var course = context.Courses.FirstOrDefault(x => x.HoursToComplete == 999);

                //Console.WriteLine($"{course?.CourseName}, {course?.Price.ToString("C")}");

                // var course = context.Courses.SingleOrDefault(x => x.HoursToComplete == 999);

                // Console.WriteLine($"{course?.CourseName}, {course?.Price.ToString("C")}");

                var courses = context.Courses.Where(x => x.Price > 3000);

                Console.WriteLine(courses.ToQueryString());

                foreach (var course in courses)
                    Console.WriteLine($"course name: {course.CourseName}, {course.HoursToComplete} hrs., {course.Price.ToString("C")}");

            }
            Console.ReadKey();
        }
    }
}