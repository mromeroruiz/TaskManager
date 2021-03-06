﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Contracts;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class ToDoService : IToDoService
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
                    GroupID = model.GroupID,
                    IsDone = false,
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
                var user =
                    ctx.Users.Single(e => e.Id == _userID.ToString());

                var query =
                    ctx
                        .ToDos
                        .Where(e => e.GroupID == user.GroupID)
                        .Select(
                            e =>
                                new ToDoListItem
                                {
                                    OwnerID = e.OwnerID,
                                    ToDoID = e.ToDoID,
                                    Title = e.Title,
                                    GroupID = e.GroupID,
                                    IsDone = e.IsDone,
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
                    .Single(e => e.ToDoID == toDoId);
                return
                    new ToDoDetails
                    {

                        ToDoID = entity.ToDoID,
                        Title = entity.Title,
                        Details = entity.Details,
                        GroupID = entity.GroupID,
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
                        .Single(e => e.ToDoID == model.ToDoID);
                entity.Title = model.Title;
                entity.Details = model.Details;
                entity.GroupID = model.GroupID;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;
                entity.IsDone = model.IsDone;

                return ctx.SaveChanges() == 1;

            }
        }

        public bool DeleteToDo(int toDoId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .ToDos
                    .Single(e => e.ToDoID == toDoId);
                ctx.ToDos.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
