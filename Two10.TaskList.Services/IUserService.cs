using System;
using Two10.TaskList.Model;

namespace Two10.TaskList.Services
{
    public interface IUserService : IDisposable
    {
        User Get(string emailAddress);

        User Create(string emailAddress, string name);

    }
}
