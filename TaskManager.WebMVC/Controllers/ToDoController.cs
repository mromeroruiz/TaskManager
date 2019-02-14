using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Data;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.WebMVC.Controllers
{
    public class ToDoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        // GET: Task
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ToDoService(userId);
            var model = service.GetToDos();

            return View(model);
        }
        // GET
        
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName");
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ToDoCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateToDoService();

            if (service.CreateToDo(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Note could not be created.");
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupName");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateToDoService();
            var model = svc.GetToDoById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateToDoService();
            var detail = service.GetToDoById(id);
            var model = new ToDoEdit
            {
                ToDoID = detail.ToDoID,
                Title = detail.Title,
                Details = detail.Details
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ToDoEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ToDoID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateToDoService();

            if (service.UpdateToDo(model))
            {
                TempData["SaveResult"] = "Your note was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your note could not be updated.");
            return View();
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateToDoService();
            var model = svc.GetToDoById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateToDoService();

            service.DeleteToDo(id);
            TempData["SaveResult"] = "Your note was deleted";

            return RedirectToAction("Index");
        }

        private ToDoService CreateToDoService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ToDoService(userId);
            return service;
        }
    }
}