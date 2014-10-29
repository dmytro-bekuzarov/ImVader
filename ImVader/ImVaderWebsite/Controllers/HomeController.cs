namespace ImVaderWebsite.Controllers
{
    using System.Web.Mvc;
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("demo")]
        public ActionResult Demo()
        {
            return View();
        }

        [Route("documentation")]
        public ActionResult Documentation()
        {
            return View();
        }

        [Route("contact")]
        public ActionResult Contact()
        {
            return View();
        }
    }
}