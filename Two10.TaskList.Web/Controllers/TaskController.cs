using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Two10.TaskList.Model;
using Two10.TaskList.Services;
using Two10.TaskList.Web.Models;

namespace Two10.TaskList.Web.Controllers
{
    public class TaskController : BaseController
    {
        public ITaskService TaskService { get; private set; }

        public TaskController(ITaskService taskService)
        {
            this.TaskService = taskService;
        }

        public TaskController()
        {
            this.ViewBag.Title = "Tasks";
            this.TaskService = ServiceFactory.CreateTaskService(this.CurrentUser);
        }

        public ActionResult Index()
        {
            this.ViewBag.SubTitle = "All tasks";
            using (this.TaskService)
            {
                var model = this.TaskService.Tasks().Select(x => Mapper.Map<TaskItemViewModel>(x));
                return View("List", model.ToArray());
            }
        }

        public ActionResult Create()
        {
            this.ViewBag.SubTitle = "Create a new task";
            return View("Edit", new TaskItemViewModel());
        }

        [HttpPost]
        public ActionResult Create(TaskItemViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            var taskItem = Mapper.Map<TaskItem>(model);
            using (this.TaskService)
            {
                this.TaskService.Save(taskItem);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            this.ViewBag.SubTitle = "Edit task";
            using (this.TaskService)
            {
                var model = this.TaskService.Get(id);
                var viewModel = Mapper.Map<TaskItemViewModel>(model);
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(TaskItemViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            using (this.TaskService)
            {
                var efModel = this.TaskService.Get(model.Id);
                Mapper.Map<TaskItemViewModel, TaskItem>(model, efModel);
                this.TaskService.Save(efModel);
            }

            return RedirectToAction("Index");

        }


    }
}
