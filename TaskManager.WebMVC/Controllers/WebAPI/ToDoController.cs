using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.WebMVC.Controllers.WebAPI
{
    [Authorize]
    [RoutePrefix("api/ToDo")]
    public class ToDoController : ApiController
    {
        private bool SetStarState(int toDoID, bool newState)
        {
            // Create the service
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ToDoService(userId);

            // Get the note
            var detail = service.GetToDoById(toDoID);

            // Create the NoteEdit model instance with the new star state
            var updatedNote =
                new ToDoEdit
                {
                    ToDoID = detail.ToDoID,
                    Title = detail.Title,
                    Details = detail.Details,
                    IsDone = newState
                };

            // Return a value indicating whether the update succeeded
            return service.UpdateToDo(updatedNote);
        }

        [Route("{id}/Star")]
        [HttpPut]
        public bool ToggleStarOn(int id) => SetStarState(id, true);

        [Route("{id}/Star")]
        [HttpDelete]
        public bool ToggleStarOff(int id) => SetStarState(id, false);

    }
}
