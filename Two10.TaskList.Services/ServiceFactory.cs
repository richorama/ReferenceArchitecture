using Two10.TaskList.Model;

namespace Two10.TaskList.Services
{
    public class ServiceFactory
    {

        public static ITaskService CreateTaskService(User user)
        {
            return new TaskService(user, new Database());
        }

        public static ITaskService CreateTaskService(User user, IDatabase databaseMock)
        {
            return new TaskService(user, databaseMock);
        }

        public static IUserService CreateUserService()
        {
            return new UserService(new Database());
        }

        public static IUserService CreateUserService(IDatabase databaseMock)
        {
            return new UserService(databaseMock);
        }
    }
}
