using System.Web.Mvc;
using Two10.TaskList.Model;
using Two10.TaskList.Services;

namespace Two10.TaskList.Mvc
{
    public class BaseController : Controller
    {
        public User CurrentUser
        {
            get
            {
                using (var userService = ServiceFactory.CreateUserService())
                {
                    return userService.Get("richard.astbury@gmail.com");
                }

            }
        }
    }
}