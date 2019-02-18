using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class ToDoListItem
    {
        public int ToDoID { get; set; }
        public bool Checkbox { get; set; }
        public string Title { get; set; }
        public Guid OwnerID { get; set; }

        [UIHint("Starred")]
        [Display(Name = "Completed?")]
        public bool IsDone { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }
        public int GroupID { get; set; }

        [Display(Name="Created")]
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
