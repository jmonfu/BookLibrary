using System.Web.Mvc;

namespace HomeBookLibrary.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Loans()
        {
            ViewBag.Title = "Loans Page";

            return View();
        }
    }
}