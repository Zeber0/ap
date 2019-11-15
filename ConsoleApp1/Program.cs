using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{

    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                Random rand = new Random();
                for (int i = 0; i < rand.Next(1, 10); i++)
                {
                    var name = 2000 + i;
                    var year = new Year { YearNumber = name };
                    if (!db.Classes.Include(x => x.Year).Any(Year => Year.Year.YearNumber == name))
                    {
                        db.Years.Add(year);
                        db.SaveChanges();
                        for (int j = 0; j < 3; j++)
                        {
                            var clas = new Class { Litera = ChoseLitera(j), Year = year };
                            db.Classes.Add(clas);
                            db.SaveChanges();
                        }
                    }
                }

                db.Classes.ToList().ForEach(x => Console.WriteLine($"{x.Litera} {x.Year.YearNumber}"));
                Console.ReadKey();
            }
        }
        private static string ChoseLitera(int l)
        {
            switch (l)
            {
                case 1:
                    return "a";
                case 2:
                    return "b";
                default:
                    return "c";
            }
        }
    }
    public class Year
    {
        [Key]
        public int Id { get; set; }
        public int YearId { get; set; }
        public int YearNumber { get; set; }
        public List<Class> Сlases { get; set; }
    }
    public class Class
    {
        [Key]
        public int Id { get; set; }
        public string Litera { get; set; }
        public Year Year { get; set; }
        public List<Student> Students { get; set; }
    }

    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Hobby { get; set; }
        public Class Class { get; set; }
    }
    public class BloggingContext : DbContext
    {
        public DbSet<Year> Years { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("testDataBase");
        }
    }
}
