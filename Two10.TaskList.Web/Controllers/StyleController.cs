using System.Web.Mvc;

namespace Two10.TaskList.Web.Controllers
{
    public class StyleController : Controller
    {
        public ActionResult Index()
        {
            this.Response.ContentType = @"text/css";
            return Content("");
        }
    }
}
