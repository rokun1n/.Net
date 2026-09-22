using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rokun1n.TaskPlanner.Domain.Models;

namespace Rokun1n.TaskPlanner.Domain.Logic
{
    public class SimpleTaskPlanner
    {
        public WorkItem[] CreatePlan(WorkItem[] items)
        {
            if (items == null)
            {
                return Array.Empty<WorkItem>();
            }

            var itemsAsList = items.ToList();
            itemsAsList.Sort(CompareWorkItems);
            return itemsAsList.ToArray();
        }

        private static int CompareWorkItems(WorkItem firstItem, WorkItem secondItem)
        {
            if (firstItem == null && secondItem == null) return 0;
            if (firstItem == null) return 1;
            if (secondItem == null) return -1;

            int priorityComparison = secondItem.Priority.CompareTo(firstItem.Priority);
            if (priorityComparison != 0)
            {
                return priorityComparison;
            }

            int dueDateComparison = firstItem.DueDate.CompareTo(secondItem.DueDate);
            if (dueDateComparison != 0)
            {
                return dueDateComparison;
            }

            return string.Compare(firstItem.Title, secondItem.Title, StringComparison.OrdinalIgnoreCase);
        }
    }
}