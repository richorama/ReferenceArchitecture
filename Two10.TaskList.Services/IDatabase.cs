using System;
using System.Data.Entity;
using Two10.TaskList.Model;

namespace Two10.TaskList.Services
{
    public interface IDatabase : IDisposable
    {
        IDbSet<TaskItem> TaskItems { get; set; }

        IDbSet<User> Users { get; set; }

        int SaveChanges();

        void SetAsModified(object entity);
    }
}
