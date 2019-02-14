using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Data
{
    public class Group
    {
        [Key]
        public int GroupID { get; set; }
        [Required]
        public string GroupName { get; set; }
        public Guid OwnerID { get; set; }
    }
}
