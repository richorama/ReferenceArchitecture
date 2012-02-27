using System;
using System.Linq;
using Two10.TaskList.Model;

namespace Two10.TaskList.Services
{
    internal class UserService : IUserService
    {
        public IDatabase Database { get; private set; }

        public UserService(IDatabase database)
        {
            if (null == database) throw new ArgumentNullException("database");

            this.Database = database;
        }

        public User Get(string emailAddress)
        {
            return this.Database.Users
                .Where(u => string.Compare(u.Email, emailAddress, true) == 0)
                .FirstOrDefault();
        }

        public User Create(string emailAddress, string name)
        {
            User user = null;
            if (null == (user = this.Get(emailAddress)))
            {
                user = new User() { Email = emailAddress, Name = name };
                this.Database.Users.Add(user);
                this.Database.SaveChanges();
            }
            return user;
        }

        public void Dispose()
        {
            this.Database.Dispose();
        }

    }
}
