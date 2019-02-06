using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class ToDoService
    {
        private readonly Guid _userID;

        public ToDoService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateToDo(ToDoCreate model)
        {
            var entity =
                new ToDo()
                {
                    OwnerID = _userID,
                    Title = model.Title,
                    Details = model.Details,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.ToDos.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<ToDoListItem> GetToDos()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .ToDos
                        .Where(e => e.OwnerID == _userID)
                        .Select(
                            e =>
                                new ToDoListItem
                                {
                                    ToDoID = e.ToDoID,
                                    Title = e.Title,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }
        public ToDoDetails GetToDoById(int toDoId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .ToDos
                    .Single(e => e.ToDoID == toDoId && e.OwnerID == _userID);
                return
                    new ToDoDetails
                    {
                        ToDoID = entity.ToDoID,
                        Title = entity.Title,
                        Details = entity.Details,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc

                    };
            }
        }

        public bool UpdateToDo(ToDoEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .ToDos
                        .Single(e => e.ToDoID == model.ToDoID && e.OwnerID == _userID);
                entity.Title = model.Title;
                entity.Details = model.Details;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;

            }
        }

        public bool DeleteToDo(int toDoId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .ToDos
                    .Single(e => e.ToDoID == toDoId && e.OwnerID == _userID);
                ctx.ToDos.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
