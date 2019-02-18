using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Contracts
{
    public interface IToDoService
    {
        bool CreateToDo(ToDoCreate model);
        IEnumerable<ToDoListItem> GetToDos();
        ToDoDetails GetToDoById(int toDoId);
        bool UpdateToDo(ToDoEdit model);
        bool DeleteToDo(int toDoId);

    }

}
