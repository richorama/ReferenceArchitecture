using Moq;
using NUnit.Framework;
using Two10.TaskList.Model;
using Two10.TaskList.Services;
using System.Linq;

namespace Two10.TaskList.Tests
{
    [TestFixture]
    public class ServicesTests
    {

        private static Mock<IDatabase> CreateDatabaseMock()
        {
            var database = new Mock<IDatabase>();

            var tasks = new InMemoryDbSet<TaskItem>();
            tasks.SetPrimaryKeyField(x => x.Id);
            database.SetupGet(x => x.TaskItems).Returns(tasks);

            var users = new InMemoryDbSet<User>();
            users.SetPrimaryKeyField(x => x.Id);
            database.SetupGet(x => x.Users).Returns(users);

            return database;
        }


        [Test]
        public void Test_TaskService_Add_Get()
        {
            var mockDatabase = CreateDatabaseMock();
            var user = new User() { Name = "Test user", Email = "test@test.com", Id = 1 };
            using (var taskService = ServiceFactory.CreateTaskService(user, mockDatabase.Object))
            {
                Assert.IsNotNull(taskService);

                var task = new Two10.TaskList.Model.TaskItem() { Name = "Get milk" };
                taskService.Save(task);
                var task2 = taskService.Get(task.Id);

                Assert.IsNotNull(task2);
                Assert.AreEqual(task, task2);
                Assert.AreEqual(user, task.User, "The service should associate the task with the user");
                Assert.AreEqual(1, taskService.AllTasks().Count());
            }
        }

        [Test]
        public void Test_UserService_Get()
        {
            var mockDatabase = CreateDatabaseMock();
            var user = mockDatabase.Object.Users.Add(new User() { Email = "test@test.com" });

            using (var userService = ServiceFactory.CreateUserService(mockDatabase.Object))
            {
                Assert.AreEqual(user, userService.Get(user.Email.ToUpper()));
            }
        }

        [Test]
        public void Test_UserService_Create()
        {
            var mockDatabase = CreateDatabaseMock();

            using (var userService = ServiceFactory.CreateUserService(mockDatabase.Object))
            {
                Assert.IsNull(userService.Get("test@test.com"));
                Assert.IsNotNull(userService.Create("test@test.com", "Test User"));
                Assert.IsNotNull(userService.Get("test@test.com"));
            }

        }




    }



}
