using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Models;
using Group = TaskManager.Data.Group;

namespace TaskManager.Services
{
    public class GroupService
    {
        private readonly Guid _userId;

        public GroupService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateGroup(GroupCreate model)
        {
            var entity =
                new Group()
                {
                    GroupName = model.GroupName
                };
                   

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Groups.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<GroupListItem> GetGroup()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Groups
                        .Select(
                            e =>
                                new GroupListItem
                                {
                                    GroupID = e.GroupID,
                                    GroupName = e.GroupName,

                                }
                        );

                return query.ToArray();
            }
        }


        public GroupDetails GetGroupById(int GroupID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Groups
                        .Single(e => e.GroupID == GroupID);
                return
                    new GroupDetails
                    {
                        GroupID = entity.GroupID,
                        GroupName = entity.GroupName,
                        
                    };
            }
        }

        public bool UpdateGroup(GroupEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Groups
                    .Single(e => e.GroupID == model.GroupID);

                entity.GroupID = model.GroupID;
                entity.GroupName = model.GroupName;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteGroup(int groupId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Groups
                        .Single(e => e.GroupID == groupId);

                ctx.Groups.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

