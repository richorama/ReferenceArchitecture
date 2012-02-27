using System.Collections.Generic;

namespace Two10.TaskList.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public ICollection<TaskItem> Tasks { get; set; }

    }
}
