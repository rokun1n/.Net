using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rokun1n.TaskPlanner.Domain.Models.Enums;

namespace Rokun1n.TaskPlanner.Domain.Models
{
    public class WorkItem
    {
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Complexity Complexity { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }

        public override string ToString()
        {
            string priorityFormatted = Priority.ToString().ToLower() + " priority";
            return $"{Title}: due {DueDate:dd.MM.yyyy}, {priorityFormatted}";
        }
    }
}