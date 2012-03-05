using System;
using Two10.TaskList.Model;
using System.Collections.Generic;

namespace Two10.TaskList.Services
{
    public interface ITaskService : IDisposable
    {
        TaskItem Get(int id);

        void Save(TaskItem newTask);

        IEnumerable<TaskItem> AllTasks();
    }
}
