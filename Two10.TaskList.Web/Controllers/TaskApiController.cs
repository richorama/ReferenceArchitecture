using System.Collections.Generic;
using System.Web.Http;
using Two10.TaskList.Model;
using Two10.TaskList.Services;
using System;

namespace Two10.TaskList.Web.Controllers
{
    public class TaskApiController : ApiController
    {
        private User CurrentUser
        {
            get
            {
                using (var userService = ServiceFactory.CreateUserService())
                {
                    return userService.Get("richard.astbury@gmail.com");
                }
            }
        }

        public IEnumerable<TaskItem> Get()
        {
            var service = ServiceFactory.CreateTaskService(this.CurrentUser);
            return service.AllTasks();
        }

        public TaskItem Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Post(TaskItem value)
        { 
        
        }

        public void Put(int id, TaskItem value)
        { 
        
        }

        public void Delete(int id)
        { 
        
        }



    }
}
