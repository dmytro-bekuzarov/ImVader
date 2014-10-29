namespace ImVaderWebsite.Controllers
{
    using System;
    using System.Web.Mvc;

    using ImVaderWebsite.Models;

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
        public ActionResult Contact(String message)
        {
            ViewBag.Message = message;
            return View();
        }

        [Route("contact/send")]
        [HttpPost]
        public ActionResult SendContactMail(ContactForm form)
        {
            String message = "Your message was sent successfully!";
            try
            {
                var mailSender = new ContactMailSender();
                mailSender.Send(form);
            }
            catch (Exception)
            {
                message = "Something went wrong";
            }
            return RedirectToAction("Contact", "Home", new { message = message });
        }
    }
}