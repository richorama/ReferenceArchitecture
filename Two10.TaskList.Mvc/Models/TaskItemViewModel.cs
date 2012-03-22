using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Two10.TaskList.Mvc.Models
{
    public class TaskItemViewModel
    {
        [HiddenInputAttribute(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}