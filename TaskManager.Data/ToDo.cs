using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Data
{
    public class ToDo
    {
        [Key]
        public int ToDoID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public Guid OwnerID { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int GroupID { get; set; }
        public bool Checkbox { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }


    }
}
