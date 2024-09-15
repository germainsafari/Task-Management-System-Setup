using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TaskController : Controller
    {
        private static List<TaskModel> taskList = new List<TaskModel>();

        // GET: Task
        public ActionResult Index(string sortOrder)
        {
            // Sort tasks by priority or creation date
            ViewBag.PrioritySortParm = String.IsNullOrEmpty(sortOrder) ? "priority_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var tasks = from t in taskList select t;

            switch (sortOrder)
            {
                case "priority_desc":
                    tasks = tasks.OrderByDescending(t => t.Priority);
                    break;
                case "Date":
                    tasks = tasks.OrderBy(t => t.CreationDate);
                    break;
                case "date_desc":
                    tasks = tasks.OrderByDescending(t => t.CreationDate);
                    break;
                default:
                    tasks = tasks.OrderBy(t => t.Priority);
                    break;
            }

            return View(tasks.ToList());
        }

        // GET: Task/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                taskList.Add(task);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // GET: Task/Edit/5
        public ActionResult Edit(int id)
        {
            var task = taskList.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound();

            return View(task);
        }

        // POST: Task/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskModel task)
        {
            var originalTask = taskList.FirstOrDefault(t => t.Id == task.Id);
            if (originalTask == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                // Update task details
                originalTask.Title = task.Title;
                originalTask.Description = task.Description;
                originalTask.AssignedUser = task.AssignedUser;
                originalTask.Priority = task.Priority;
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // GET: Task/Delete/5
        public ActionResult Delete(int id)
        {
            var task = taskList.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound();

            return View(task);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var task = taskList.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound();

            taskList.Remove(task);
            return RedirectToAction("Index");
        }
    }
}
