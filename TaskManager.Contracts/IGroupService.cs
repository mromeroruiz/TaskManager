using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Contracts
{
    public interface IGroupService
    {
        bool CreateGroup(GroupCreate model);
        IEnumerable<GroupListItem> GetGroup();
        GroupDetails GetGroupById(int GroupID);
        bool UpdateGroup(GroupEdit model);
        bool DeleteGroup(int groupId);
        bool JoinGroup(int groupId);

    }
}
