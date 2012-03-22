using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Two10.TaskList.Model;
using Two10.TaskList.Mvc.Models;
using Two10.TaskList.Services;

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

        private ITaskService service = null;

        public TaskApiController()
        {
            service = ServiceFactory.CreateTaskService(this.CurrentUser);
        }

        public IEnumerable<TaskItemViewModel> Get()
        {
            return service.AllTasks().Where(x => !x.Complete).Select(x => Mapper.Map<TaskItemViewModel>(x));
        }

        public TaskItemViewModel Get(int id)
        {
            return Mapper.Map<TaskItemViewModel>(service.Get(id));
        }

        public void Post(TaskItemViewModel value)
        {
            var model = Mapper.Map<TaskItem>(value);
            service.Save(model);
        }

        public void Put(int id, TaskItemViewModel value)
        {
            var efModel = service.Get(id);
            if (null == efModel)
            {
                return;
            }

            Mapper.Map<TaskItemViewModel, TaskItem>(value, efModel);
            service.Save(efModel);
        }

        public void Delete(int id)
        {
            var model = service.Get(id);
            if (null == model)
            {
                return;
            }
            model.Complete = true;
            service.Save(model);
        }



    }
}
