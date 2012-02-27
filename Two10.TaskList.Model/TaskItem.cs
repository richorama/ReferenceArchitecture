using System;

namespace Two10.TaskList.Model
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Complete { get; set; }

        public DateTime Created { get; set; }

        public User User { get; set; }

    }
}
