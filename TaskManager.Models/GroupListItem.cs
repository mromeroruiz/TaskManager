using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class GroupListItem
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }

        public override string ToString() => GroupName;
    }
}
