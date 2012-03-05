using System;
using System.Collections.Generic;
using System.Linq;
using Two10.TaskList.Model;

namespace Two10.TaskList.Services
{
    internal class TaskService : ITaskService
    {
        public User User { get; private set; }

        public IDatabase Database { get; private set; }

        public IQueryable<TaskItem> UserTasks
        {
            get
            {
                return this.Database.TaskItems.Where(t => t.User.Id == this.User.Id);
            }
        }

        public TaskService(User user, IDatabase database)
        {
            if (null == database) throw new ArgumentNullException("database");
            if (null == user) throw new ArgumentNullException("user");

            this.User = user;
            this.Database = database;
        }

        public TaskItem Get(int id)
        {
            return this.UserTasks.Where(t => t.Id == id).FirstOrDefault();
        }

        public void Save(TaskItem newTask)
        {
            newTask.User = this.User;
            this.Database.Users.Attach(this.User);
            if (newTask.Id == 0)
            {
                newTask.Created = DateTime.UtcNow;
                this.Database.TaskItems.Add(newTask);
            }
            else
            {
                this.Database.TaskItems.Attach(newTask);
                this.Database.SetAsModified(newTask);
            }
            this.Database.SaveChanges();
        }

        public IEnumerable<TaskItem> AllTasks()
        {
            return this.UserTasks;
        }

        public void Dispose()
        {
            if (null != this.Database)
            {
                this.Database.Dispose();
            }
        }
    }
}
