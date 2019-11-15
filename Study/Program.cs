using System;
using System.Collections.Generic;
using System.Linq;

namespace Study
{
    class Calendar
    {
        public List<CalendarTask> Tasks;
        public Calendar()
        {
            Tasks = new List<CalendarTask>();
        }
        public void AddTask(CalendarTask task)
        {
            task.Id = Tasks.DefaultIfEmpty(new CalendarTask { Id = 0 }).Max(x => x.Id) + 1;
            Tasks.Add(task);
        }
        public void RemoveTask(int id)
        {
            Tasks.FirstOrDefault(task => task.Id == id);
        }
        public void ShowTasks(DayOfWeek dayOfWeek)
        {
            Tasks.Where(task => task.TargetDay == dayOfWeek)
                .OrderBy(task => task.TargetDay).ToList()
                .ForEach(task => Console.WriteLine($"{task.Id} {task.TargetDay} {task.Name}"));
        }
    }
    class CalendarTask
    {
        public int Id;
        public string Name;
        public DayOfWeek TargetDay;
    }
    enum DayOfWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
    class Program
    {
        static void Main(string[] args)
        {
            var calendar = new Calendar();
            while (true)
            {
                var stringDateOfWeek = Console.ReadLine();
                if (!Enum.TryParse<DayOfWeek>(stringDateOfWeek, out var dayOfWeek)) continue;
                var name = Console.ReadLine();

                calendar.AddTask(new CalendarTask { TargetDay = dayOfWeek, Name = name });

                calendar.ShowTasks(dayOfWeek);

                calendar.RemoveTask(1);
            }
        }
    }
}
