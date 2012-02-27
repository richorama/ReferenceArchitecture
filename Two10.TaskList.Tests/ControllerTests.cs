using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using Two10.TaskList.Model;
using Two10.TaskList.Services;
using Two10.TaskList.Web;
using Two10.TaskList.Web.Controllers;
using Two10.TaskList.Web.Models;

namespace Two10.TaskList.Tests
{
    [TestFixture]
    public class ControllerTests
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            AutoMapperConfiguration.Configure();
        }

        [Test]
        public void Test_TaskController()
        {
            var taskService = new Moq.Mock<ITaskService>();
            taskService.Setup<IEnumerable<TaskItem>>(x => x.Tasks()).Returns(new List<TaskItem> { new TaskItem() { Name = "New Task" } });

            var taskController = new TaskController(taskService.Object);
            var result = taskController.Index();
            Assert.IsNotNull(result as ViewResult);
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult.Model);
            Assert.IsNotNull(viewResult.Model as IEnumerable<TaskItemViewModel>);
            Assert.AreEqual("New Task", (viewResult.Model as IEnumerable<TaskItemViewModel>).FirstOrDefault().Name);
        }

        [Test]
        public void Test_TaskController_Create()
        {
            var taskService = new Moq.Mock<ITaskService>();

            var taskController = new TaskController(taskService.Object);
            var result = taskController.Create();
            Assert.IsNotNull(result as ViewResult);
            Assert.IsNotNull((result as ViewResult).Model as TaskItemViewModel);

            var result2 = taskController.Create(new TaskItemViewModel() { Name = "New Task" });
            Assert.IsNotNull(result2 as RedirectToRouteResult);
        }

        [Test]
        public void Test_TaskController_Edit()
        {
            var taskService = new Moq.Mock<ITaskService>();
            var item = new TaskItem() { Id = 1, Name = "Test Task" };
            taskService.Setup<TaskItem>(x => x.Get(1)).Returns(item);

            var taskController = new TaskController(taskService.Object);


            var result = taskController.Edit(1);
            Assert.IsNotNull(result as ViewResult);
            var model = (result as ViewResult).Model as TaskItemViewModel;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Id);
            Assert.AreEqual("Test Task", model.Name);

            model.Name = "Altered Task";
            var result2 = taskController.Edit(model);
            Assert.IsNotNull(result2 as RedirectToRouteResult);

            var result3 = taskController.Edit(1);
            var model3 = (result3 as ViewResult).Model as TaskItemViewModel;
            Assert.AreEqual(1, model.Id);
            Assert.AreEqual("Altered Task", model.Name);

        }


    }
}
