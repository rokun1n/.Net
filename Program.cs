using System;
using System.Collections.Generic;
using Rokun1n.TaskPlanner.Domain.Logic;
using Rokun1n.TaskPlanner.Domain.Models;
using Rokun1n.TaskPlanner.Domain.Models.Enums;

namespace Rokun1n.TaskPlanner.ConsoleRunner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var workItems = new List<WorkItem>();

            Console.WriteLine("=== Введення задач у планувальник ===");

            while (true)
            {
                Console.WriteLine("\nДодати нову задачу? (y/n):");
                string? answer = Console.ReadLine()?.Trim().ToLower();
                if (answer != "y")
                {
                    break;
                }

                var item = new WorkItem();
                item.CreationDate = DateTime.Now;

                Console.Write("Введіть назву (Title): ");
                item.Title = Console.ReadLine() ?? string.Empty;

                Console.Write("Введіть опис (Description): ");
                item.Description = Console.ReadLine() ?? string.Empty;

                Console.Write("Введіть дату виконання (DueDate) у форматі dd.MM.yyyy: ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime dueDate))
                {
                    item.DueDate = dueDate;
                }
                else
                {
                    item.DueDate = DateTime.Now;
                    Console.WriteLine("Некоректний формат дати, встановлено поточну дату.");
                }

                Console.Write("Введіть пріоритет (None, Low, Medium, High, Urgent): ");
                if (Enum.TryParse<Priority>(Console.ReadLine(), true, out Priority priority))
                {
                    item.Priority = priority;
                }
                else
                {
                    item.Priority = Priority.None;
                    Console.WriteLine("Некоректний пріоритет, встановлено None.");
                }

                Console.Write("Введіть складність (None, Minutes, Hours, Days, Weeks): ");
                if (Enum.TryParse<Complexity>(Console.ReadLine(), true, out Complexity complexity))
                {
                    item.Complexity = complexity;
                }
                else
                {
                    item.Complexity = Complexity.None;
                    Console.WriteLine("Некоректну складність, встановлено None.");
                }

                workItems.Add(item);
            }

            var planner = new SimpleTaskPlanner();
            WorkItem[] plan = planner.CreatePlan(workItems.ToArray());

            Console.WriteLine("\n=== Впорядкований список задач ===");
            foreach (var item in plan)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}