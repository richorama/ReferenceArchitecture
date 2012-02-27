using System;
using System.Data;
using System.Data.Entity;
using Two10.TaskList.Model;

namespace Two10.TaskList.Services
{
    internal sealed class Database : DbContext, IDatabase, IDisposable
    {
        public IDbSet<TaskItem> TaskItems { get; set; }

        public IDbSet<User> Users { get; set; }

        public void SetAsModified(object entity)
        {
            this.Entry(entity).State = EntityState.Modified;
        }
    }
}
