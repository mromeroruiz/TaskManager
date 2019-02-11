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
    [Authorize]
    public class GroupController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Group
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new GroupService(userId);
            var model = service.GetGroup();
            return View(model);
        }

        // Get Group/Create

        public ActionResult Create()
        {
            return View();
        }

        //POST Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GroupCreate model)
        {
            if (!ModelState.IsValid) return View(model);


            var service = CreateGroupService();

            if (service.CreateGroup(model))
            {
                TempData["SaveResult"] = "Your Group was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Group could not be created.");

            return View(model);

        }

        public ActionResult Details(int id)
        {
            var svc = CreateGroupService();
            var model = svc.GetGroupById(id);

            return View(model);
        }


        public ActionResult Edit(int id)
        {
            var service = CreateGroupService();
            var detail = service.GetGroupById(id);
            var model =
                new GroupEdit
                {
                    GroupID = detail.GroupID,
                    GroupName = detail.GroupName
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, GroupEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.GroupID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateGroupService();

            if(service.UpdateGroup(model))
            {
                TempData["SaveResult"] = "Your group was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your group could not be updated.");
            return View();
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateGroupService();
            var model = svc.GetGroupById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateGroupService();

            service.DeleteGroup(id);

            TempData["SaveResult"] = "Your group was deleted";

            return RedirectToAction("Index");
        }

        private GroupService CreateGroupService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new GroupService(userId);
            return service;
        }
    }
}